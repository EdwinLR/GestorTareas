using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    [Authorize(Roles = "Coordinator,Admin")]
    public class GendersController : Controller
    {
        private readonly IGenderRepository _repository;

        public GendersController(IGenderRepository repository)
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

            var gender = await _repository.GetByIdAsync(id.Value);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(gender);
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _repository.GetByIdAsync(id.Value);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gender gender)
        {
            if (id != gender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(gender);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.ExistAsync(gender.Id))
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
            return View(gender);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _repository.GetByIdAsync(id.Value);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gender = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(gender);
            return RedirectToAction(nameof(Index));
        }
    }
}