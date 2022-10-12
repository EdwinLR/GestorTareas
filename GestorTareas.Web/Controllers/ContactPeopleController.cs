using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    [Authorize(Roles = "Coordinator,Admin")]
    public class ContactPeopleController : Controller
    {
        private readonly DataContext _context;

        public ContactPeopleController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactPeople.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPerson = await _context.ContactPeople
                .FirstOrDefaultAsync(cP => cP.Id == id);
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
                _context.Add(contactPerson);
                await _context.SaveChangesAsync();
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

            var contactPerson = await _context.ContactPeople.FindAsync(id);
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
                    _context.Update(contactPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactPersonExists(contactPerson.Id))
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

            var contactPerson = await _context.ContactPeople
                .FirstOrDefaultAsync(cP => cP.Id == id);
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
            var contactPerson = await _context.ContactPeople.FindAsync(id);
            _context.ContactPeople.Remove(contactPerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactPersonExists(int id)
        {
            return _context.ContactPeople.Any(cP => cP.Id == id);
        }
    }
}