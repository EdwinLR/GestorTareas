using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class InstitutesController : Controller
    {
        private readonly IInstituteRepository _repository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICombosHelper combosHelper;

        public InstitutesController(IInstituteRepository repository,
            ICountryRepository countryRepository,
            ICombosHelper combosHelper)
        {
            _repository = repository;
            _countryRepository = countryRepository;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Index()
        {
            return View(_repository.GetAllInstitutesWithCountriesAndContactPeople());
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = _repository.GetInstituteWithCountryAndContactPersonById(id.Value);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Create()
        {
            var model = new InstituteViewModel
            {
                Countries = this.combosHelper.GetComboCountries()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InstituteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var institute = new Institute
                {
                    Name = model.Name,
                    ContactPhone = model.ContactPhone,
                    StreetName = model.StreetName,
                    StreetNumber = model.StreetNumber,
                    District = model.District,
                    City = model.City,
                    Country = this._countryRepository.GetMasterById(model.CountryId)
                };

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = _repository.GetInstituteWithCountryAndContactPersonById(id.Value);
            if (institute == null)
            {
                return NotFound();
            }
            var model = new InstituteViewModel
            {
                Id = institute.Id,
                Name = institute.Name,
                ContactPhone = institute.ContactPhone,
                StreetName = institute.StreetName,
                StreetNumber = institute.StreetNumber,
                District = institute.District,
                City = institute.City,
                Country = institute.Country,
                CountryId = institute.Country.Id,
                Countries = this.combosHelper.GetComboCountries()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InstituteViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = _repository.GetInstituteWithCountryAndContactPersonById(id.Value);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institute = _repository.GetInstituteWithCountryAndContactPersonById(id);

            if (institute.ContactPeople.Count != 0 || institute.Convocations.Count != 0)
            {
                return RedirectToAction("Delete",id);
            }

            await _repository.DeleteAsync(institute);

            return RedirectToAction(nameof(Index));
        }

    }
}
