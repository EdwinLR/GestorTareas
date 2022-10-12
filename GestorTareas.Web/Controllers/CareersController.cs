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
    public class CareersController : Controller
    {
        private readonly DataContext _context;

        public CareersController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Careers.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .FirstOrDefaultAsync(c => c.Id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Career career)
        {
            if (ModelState.IsValid)
            {
                _context.Add(career);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(career);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }
            return View(career);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Career career)
        {
            if (id != career.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(career);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareerExists(career.Id))
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
            return View(career);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var career = await _context.Careers.FindAsync(id);
            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareerExists(int id)
        {
            return _context.Careers.Any(c => c.Id == id);
        }
    }
}
