using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    [Authorize(Roles = "Coordinator,Admin")]
    public class PrioritiesController : Controller
    {
        private readonly IPriorityRepository _repository;

        public PrioritiesController(IPriorityRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _repository.GetByIdAsync(id.Value);
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
                await _repository.CreateAsync(priority);
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

            var priority = await _repository.GetByIdAsync(id.Value);
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
                    await _repository.UpdateAsync(priority);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.ExistAsync(priority.Id))
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

            var priority = await _repository.GetByIdAsync(id.Value);
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
            var priority = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(priority);
            return RedirectToAction(nameof(Index));
        }
    }
}