using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class ConvocationsController : Controller
    {
        private readonly IInstituteRepository instituteRepository;
        private readonly ICombosHelper combosHelper;

        public ConvocationsController(
            IInstituteRepository instituteRepository,
            ICombosHelper combosHelper)
        {
            this.instituteRepository = instituteRepository;
            this.combosHelper = combosHelper;
        }

        public IActionResult Index()
        {
            return View(instituteRepository.GetAllConvocations());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = instituteRepository.GetConvocationById(id.Value);
            if (convocation == null)
            {
                return NotFound();
            }

            return View(convocation);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Convocation convocation)
        {
            if (ModelState.IsValid)
            {
                await instituteRepository.AddConvocationAsync(convocation);
                return RedirectToAction(nameof(Index));
            }
            return View(convocation);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = instituteRepository.GetConvocationById(id.Value);
            if (convocation == null)
            {
                return NotFound();
            }
            return View(convocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Convocation convocation)
        {
            if (id != convocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await instituteRepository.UpdateConvocationAsync(convocation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await instituteRepository.ExistConvocationAsync(convocation.Id))
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
            return View(convocation);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = instituteRepository.GetConvocationById(id.Value);
            if (convocation == null)
            {
                return NotFound();
            }

            return View(convocation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var convocation = instituteRepository.GetConvocationById(id);
            await instituteRepository.DeleteConvocationAsync(convocation);
            return RedirectToAction(nameof(Index));
        }
    }
}
