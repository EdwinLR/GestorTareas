using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    [Authorize(Roles = "Coordinator,Admin")]
    public class ContactPeopleController : Controller
    {
        private readonly IInstituteRepository repository;

        public ContactPeopleController(IInstituteRepository context)
        {
            repository = context;
        }

        public IActionResult Index()
        {
            return View(repository.GetAllContactPeople());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = repository.GetContactPersonById(id.Value);
            if (contactPerson == null)
            {
                return NotFound();
            }

            return View(contactPerson);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                await repository.AddContactPersonAsync(contactPerson);
                return RedirectToAction(nameof(Index));
            }
            return View(contactPerson);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = repository.GetContactPersonById(id.Value);
            if (contactPerson == null)
            {
                return NotFound();
            }
            return View(contactPerson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactPerson contactPerson)
        {
            if (id != contactPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateContactPersonAsync(contactPerson);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await repository.ExistContactPersonAsync(contactPerson.Id))
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
            return View(contactPerson);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = repository.GetContactPersonById(id.Value);
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
            var contactPerson = repository.GetContactPersonById(id);
            await repository.DeleteContactPersonAsync(contactPerson);
            return RedirectToAction(nameof(Index));
        }
    }
}