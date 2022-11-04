using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class ConvocationsController : Controller
    {
        private readonly IConvocationRepository _repository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly ICombosHelper combosHelper;

        public ConvocationsController(IConvocationRepository repository,
            IInstituteRepository instituteRepository,
            ICombosHelper combosHelper)
        {
            _repository = repository;
            _instituteRepository = instituteRepository;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Index()
        {
            return View(_repository.GetAllConvocationsWithInstitutesCountriesAndContactPeople());
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = await _repository.GetConvocationWithInstituteCountryAndContactPersonAsync(id.Value);
            if (convocation == null)
            {
                return NotFound();
            }

            return View(convocation);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Create()
        {
            var model = new ConvocationViewModel
            {
                Insitutes = this.combosHelper.GetComboInstitutes()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConvocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var convocation = new Convocation
                {
                    StartingDate = model.StartingDate,
                    EndingDate = model.EndingDate,
                    Summary = model.Summary,
                    Requirements = model.Requirements,
                    Prizes = model.Prizes,
                    ConvocationUrl = model.ConvocationUrl,
                    Institute = await this._instituteRepository.GetByIdAsync(model.InstituteId),
                };

                await _repository.CreateAsync(convocation);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = await _repository.GetConvocationWithInstituteCountryAndContactPersonAsync(id.Value);
            if (convocation == null)
            {
                return NotFound();
            }
            var model = new ConvocationViewModel
            {
                Id = convocation.Id,
                StartingDate = convocation.StartingDate,
                EndingDate = convocation.EndingDate,
                Summary = convocation.Summary,
                Requirements = convocation.Requirements,
                Prizes = convocation.Prizes,
                ConvocationUrl = convocation.ConvocationUrl,
                Institute = convocation.Institute,
                InstituteId = convocation.Institute.Id,
                Insitutes = this.combosHelper.GetComboInstitutes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConvocationViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.ExistAsync(model.Id))
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
            return View(model);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convocation = await _repository.GetConvocationWithInstituteCountryAndContactPersonAsync(id.Value);
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
            var convocation = await _repository.GetConvocationWithInstituteCountryAndContactPersonAsync(id);
            await _repository.DeleteAsync(convocation);
            return RedirectToAction(nameof(Index));
        }
    }
}
