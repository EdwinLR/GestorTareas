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
    [Authorize(Roles = "Coordinator,Admin")]
    public class ContactPeopleController : Controller
    {
        private readonly IContactPersonRepository contactPersonRepository;
        private readonly ICombosHelper combosHelper;
        private readonly IInstituteRepository instituteRepository;

        public ContactPeopleController(IContactPersonRepository contactPersonRepository, ICombosHelper combosHelper, IInstituteRepository instituteRepository)
        {
            this.contactPersonRepository = contactPersonRepository;
            this.combosHelper = combosHelper;
            this.instituteRepository = instituteRepository;
        }

        public IActionResult Index()
        {
            return View(contactPersonRepository.GetAllContactPeopleWithInstitutes());
        }

        public IActionResult Create()
        {
            var contactPerson = new ContactPersonViewModel
            {
                Institutes = this.combosHelper.GetComboInstitutes()
            };

            return View(contactPerson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactPersonViewModel contactPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                var contactPerson = new ContactPerson
                {
                    FirstName = contactPersonViewModel.FirstName,
                    FatherLastName = contactPersonViewModel.FatherLastName,
                    MotherLastName = contactPersonViewModel.MotherLastName,
                    Email = contactPersonViewModel.Email,
                    PhoneNumber = contactPersonViewModel.PhoneNumber,
                    Institute = this.instituteRepository.GetInstituteById(contactPersonViewModel.InstituteId)
                };

                await contactPersonRepository.CreateAsync(contactPerson);
                return RedirectToAction(nameof(Index));
            }
            return View(contactPersonViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await contactPersonRepository.GetContactPersonWithInstituteByIdAsync(id.Value);

            var model = new ContactPersonViewModel
            {
                FirstName = contactPerson.FirstName,
                FatherLastName = contactPerson.FatherLastName,
                MotherLastName = contactPerson.MotherLastName,
                Email = contactPerson.Email,
                PhoneNumber = contactPerson.PhoneNumber,
                InstituteId = contactPerson.Institute.Id,
                Institutes = this.combosHelper.GetComboInstitutes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactPersonViewModel contactPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                var contactPerson = new ContactPerson
                {
                    Id = contactPersonViewModel.Id,
                    FirstName = contactPersonViewModel.FirstName,
                    FatherLastName = contactPersonViewModel.FatherLastName,
                    MotherLastName = contactPersonViewModel.MotherLastName,
                    Email = contactPersonViewModel.Email,
                    PhoneNumber = contactPersonViewModel.PhoneNumber,
                    Institute = this.instituteRepository.GetInstituteById(contactPersonViewModel.InstituteId)
                };

                await this.contactPersonRepository.UpdateAsync(contactPerson);
                return RedirectToAction(nameof(Index));
            }
            return View(contactPersonViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await contactPersonRepository.GetContactPersonWithInstituteByIdAsync(id.Value);
            if (contactPerson == null)
            {
                return NotFound();
            }

            return View(contactPerson);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactPerson = await contactPersonRepository.GetByIdAsync(id);
            await contactPersonRepository.DeleteAsync(contactPerson);
            return RedirectToAction(nameof(Index));
        }
    }
}