using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                Countries = this.combosHelper.GetComboCountries()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstituteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var instituteDetailTemps = await _repository.GetAllInstituteDetailTemps().ToListAsync();
                if (instituteDetailTemps == null || instituteDetailTemps.Count() == 0)
                    return NotFound();

                var details = instituteDetailTemps.Select(idt => 
                _repository.GetContactPersonById(idt.ContactPerson.Id)).ToList();

                var convocationDetailTemps = await _repository.GetAllConvocationDetailTemps().ToListAsync();
                if (convocationDetailTemps == null || convocationDetailTemps.Count() == 0)
                    return NotFound();

                var convocationDetails = convocationDetailTemps.Select(idt =>
                _repository.GetConvocationById(idt.Convocation.Id)).ToList();



                var institute = new Institute
                {
                    Name = model.Name,
                    ContactPhone = model.ContactPhone,
                    StreetName = model.StreetName,
                    StreetNumber = model.StreetNumber,
                    District = model.District,
                    City = model.City,
                    Country = await this._countryRepository.GetDetailByIdAsync(model.CountryId),
                    ContactPeople = details,
                    Convocations = convocationDetails
                };

                await _repository.CreateInstituteAsync(institute, instituteDetailTemps, convocationDetailTemps);
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


        //ContactPerson Methods
        public IActionResult AddContactPerson()
        {
            var model = new AddContactPersonViewModel
            {
                ContactPersonId = -1,
                ContactPeopleList = combosHelper.GetComboContacts(),
                AssignedContactPeople = _repository.GetAllInstituteDetailTemps()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddContactPerson(AddContactPersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contactPerson = _repository.GetContactPersonById(model.ContactPersonId);
                if (contactPerson == null)
                    NotFound();
                var instituteDetailTemp = _repository.GetInstituteDetailTempByContactId(contactPerson.Id);
                if (instituteDetailTemp == null)
                {
                    instituteDetailTemp = new ContactDetailTemp
                    {
                        ContactPerson = contactPerson
                    };
                    _repository.AddInstituteDetailTemp(instituteDetailTemp);
                }
                return RedirectToAction("AddContactPerson");
            }
            return View(model);
        }


        public IActionResult DeleteContactPerson(int? id)
        {
            if (id == null)
                return NotFound();

            var instituteDetailTemp = _repository.GetInstituteDetailTempById(id.Value);
            if (instituteDetailTemp == null)
                return NotFound();

            _repository.DeleteInstituteDetailTemp(instituteDetailTemp);
            return RedirectToAction("AddContactPerson");
        }

        //Métodos para Convocation
        public IActionResult AddConvocations()
        {
            var model = new AddConvocationViewModel
            {
                ConvocationId = -1,
                ConvocationList = combosHelper.GetComboConvocations(),
                ConvocationDetails = _repository.GetAllConvocationDetailTemps()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddConvocations(AddConvocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var convocation = _repository.GetConvocationById(model.ConvocationId);
                if (convocation == null)
                    NotFound();
                var convocationDetailTemp = _repository.GetInstituteDetailTempByConvocationId(convocation.Id);
                if (convocationDetailTemp == null)
                {
                    convocationDetailTemp = new ConvocationDetailTemp
                    {
                        Convocation = convocation
                    };
                    _repository.AddConvocationDetailTemp(convocationDetailTemp);
                }
                return RedirectToAction("AddConvocations");
            }
            return View(model);
        }

        public IActionResult DeleteConvocation(int? id)
        {
            if (id == null)
                return NotFound();

            var convocationDetailTemp = _repository.GetInstituteDetailTempByConvocationId(id.Value);
            if (convocationDetailTemp == null)
                return NotFound();

            _repository.DeleteConvocationDetailTemp(convocationDetailTemp);
            return RedirectToAction("AddConvocations");
        }
    }
}
