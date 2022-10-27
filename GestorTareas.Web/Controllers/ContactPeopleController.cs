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
        private readonly IContactPersonRepository repository;

        public ContactPeopleController(IContactPersonRepository context)
        {
            repository = context;
        }

        public IActionResult Index()
        {
            return View(repository.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await repository.GetByIdAsync(id.Value);
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
                await repository.CreateAsync(contactPerson);
                return RedirectToAction(nameof(Index));
            }
            return View(contactPerson);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await repository.GetByIdAsync(id.Value);
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
                    await repository.UpdateAsync(contactPerson);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await repository.ExistAsync(contactPerson.Id))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await repository.GetByIdAsync(id.Value);
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
            var contactPerson = await repository.GetByIdAsync(id);
            await repository.DeleteAsync(contactPerson);
            return RedirectToAction(nameof(Index));
        }
    }
}