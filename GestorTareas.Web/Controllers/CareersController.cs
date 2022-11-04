using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    [Authorize(Roles = "Coordinator,Admin")]
    public class CareersController : Controller
    {
        private readonly ICareerRepository repository;
        public readonly IStudentRepository studentRepository;

        public CareersController(ICareerRepository repository, IStudentRepository studentRepository)
        {
            this.repository = repository;
            this.studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return View(this.repository.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careerStudents = this.studentRepository.GetStudentsByCareer(id.Value);

            return View(careerStudents);
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
                await this.repository.CreateAsync(career);
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

            var career = await this.repository.GetByIdAsync(id.Value);
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
                    await this.repository.UpdateAsync(career);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.repository.ExistAsync(career.Id))
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

            var career = await this.repository.GetByIdAsync(id.Value);
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
            var career = await this.repository.GetByIdAsync(id);
            await this.repository.DeleteAsync(career);
            return RedirectToAction(nameof(Index));
        }
    }
}
