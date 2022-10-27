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
        private readonly IContactPersonRepository _contactPersonRepository;
        private readonly ICombosHelper combosHelper;

        public InstitutesController(IInstituteRepository repository,
            ICountryRepository countryRepository,
            IContactPersonRepository contactPersonRepository,
            ICombosHelper combosHelper)
        {
            _repository = repository;
            _countryRepository = countryRepository;
            _contactPersonRepository = contactPersonRepository;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Index()
        {
            return View(_repository.GetAllInstitutesWithCountriesAndContactPeople());
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _repository.GetInstituteWithCountryAndContactPersonAsync(id.Value);
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
                Countries = this.combosHelper.GetComboCountries(),
                ContactPeople = this.combosHelper.GetComboContacts()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstituteViewModel model)
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
                    Country = await this._countryRepository.GetByIdAsync(model.CountryId),
                    ContactPerson = await this._contactPersonRepository.GetByIdAsync(model.ContactPersonId)
                };

                await _repository.CreateAsync(institute);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _repository.GetInstituteWithCountryAndContactPersonAsync(id.Value);
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
                Countries = this.combosHelper.GetComboCountries(),
                ContactPerson = institute.ContactPerson,
                ContactPersonId = institute.ContactPerson.Id,
                ContactPeople = this.combosHelper.GetComboContacts()
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _repository.GetInstituteWithCountryAndContactPersonAsync(id.Value);
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
            var institute = await _repository.GetInstituteWithCountryAndContactPersonAsync(id);
            await _repository.DeleteAsync(institute);
            return RedirectToAction(nameof(Index));
        }
    }
}
