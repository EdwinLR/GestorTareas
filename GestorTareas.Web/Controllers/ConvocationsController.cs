using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class ConvocationsController : Controller
    {
        private readonly IInstituteRepository instituteRepository;
        private readonly ICombosHelper combosHelper;
        private readonly IConvocationRepository convocationRepository;

        public ConvocationsController(
            IInstituteRepository instituteRepository,
            ICombosHelper combosHelper, IConvocationRepository convocationRepository)
        {
            this.instituteRepository = instituteRepository;
            this.combosHelper = combosHelper;
            this.convocationRepository = convocationRepository;
        }

        public IActionResult Index()
        {
            return View(convocationRepository.GetAllConvocationsWithInstitutes());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = await convocationRepository.GetConvocationWithInstituteByIdAsync(id.Value);
            if (convocation == null)
            {
                return NotFound();
            }

            return View(convocation);
        }

        public IActionResult Create()
        {
            var convocation = new ConvocationViewModel
            {
                Institutes = this.combosHelper.GetComboInstitutes()
            };

            return View(convocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConvocationViewModel convocationViewModel)
        {
            if (ModelState.IsValid)
            {
                var convocation = new Convocation
                {
                    Summary = convocationViewModel.Summary,
                    StartingDate = convocationViewModel.StartingDate,
                    EndingDate = convocationViewModel.EndingDate,
                    Prizes = convocationViewModel.Prizes,
                    ConvocationUrl = convocationViewModel.ConvocationUrl,
                    Requirements = convocationViewModel.Requirements,
                    Institute = this.instituteRepository.GetInstituteById(convocationViewModel.InstituteId)
                };

                await convocationRepository.CreateAsync(convocation);
                return RedirectToAction(nameof(Index));
            }
            return View(convocationViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = await convocationRepository.GetConvocationWithInstituteByIdAsync(id.Value);

            var model = new ConvocationViewModel
            {
                Id = convocation.Id,
                Summary = convocation.Summary,
                StartingDate = convocation.StartingDate,
                EndingDate = convocation.EndingDate,
                Prizes = convocation.Prizes,
                Requirements = convocation.Requirements,
                ConvocationUrl = convocation.ConvocationUrl,
                InstituteId = convocation.Institute.Id,
                Institutes = this.combosHelper.GetComboInstitutes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConvocationViewModel convocationViewModel)
        {
            if (ModelState.IsValid)
            {
                var convocation = new Convocation
                {
                    Id = convocationViewModel.Id,
                    Summary = convocationViewModel.Summary,
                    StartingDate = convocationViewModel.StartingDate,
                    EndingDate = convocationViewModel.EndingDate,
                    Prizes = convocationViewModel.Prizes,
                    Requirements = convocationViewModel.Requirements,
                    ConvocationUrl = convocationViewModel.ConvocationUrl,
                    Institute = this.instituteRepository.GetInstituteById(convocationViewModel.InstituteId),
                };

                await convocationRepository.UpdateAsync(convocation);
                return RedirectToAction(nameof(Index));
            }
            return View(convocationViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = await convocationRepository.GetConvocationWithInstituteByIdAsync(id.Value);
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
            var convocation = await convocationRepository.GetByIdAsync(id);
            await convocationRepository.DeleteAsync(convocation);
            return RedirectToAction(nameof(Index));
        }
    }
}
