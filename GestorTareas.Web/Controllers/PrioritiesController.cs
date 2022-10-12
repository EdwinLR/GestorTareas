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
    public class PrioritiesController : Controller
    {
        private readonly DataContext _context;

        public PrioritiesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Priorities.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _context.Priorities
                .FirstOrDefaultAsync(p => p.Id == id);
            if (priority == null)
            {
                return NotFound();
            }

            return View(priority);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Priority priority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priority);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _context.Priorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }
            return View(priority);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Priority priority)
        {
            if (id != priority.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriorityExists(priority.Id))
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
            return View(priority);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _context.Priorities
                .FirstOrDefaultAsync(p => p.Id == id);
            if (priority == null)
            {
                return NotFound();
            }

            return View(priority);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priority = await _context.Priorities.FindAsync(id);
            _context.Priorities.Remove(priority);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriorityExists(int id)
        {
            return _context.Priorities.Any(p => p.Id == id);
        }
    }
}