using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ICombosHelper combosHelper;
        private readonly DataContext context;
        private readonly IProjectRepository projectRepository;
        private readonly IInstituteRepository instituteRepository;

        public ProjectsController(ICombosHelper combosHelper, DataContext context, IProjectRepository projectRepository, IInstituteRepository instituteRepository)
        {
            this.combosHelper = combosHelper;
            this.context = context;
            this.projectRepository = projectRepository;
            this.instituteRepository = instituteRepository;
        }

        public IActionResult Index()
        {
            return View(projectRepository.GetProjectsWithConvocation());
        }

        public IActionResult Create()
        {
            var model = new ProjectViewModel
            {
                Convocations = this.combosHelper.GetComboConvocations()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    ProjectName = model.ProjectName,
                    Convocation = this.instituteRepository.GetConvocationById(model.ConvocationId)
                };

                await this.projectRepository.CreateAsync(project);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await projectRepository.GetProjectWithConvocationByIdAsync(id.Value);

            var model = new ProjectViewModel
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                ConvocationId = project.Convocation.Id,
                Convocations = this.combosHelper.GetComboConvocations()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Id = model.Id,
                    ProjectName = model.ProjectName,
                    Convocation = this.instituteRepository.GetConvocationById(model.ConvocationId),
                };

                await projectRepository.UpdateAsync(project);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await projectRepository.GetProjectWithConvocationByIdAsync(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await projectRepository.GetByIdAsync(id);
            await projectRepository.DeleteAsync(project);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await projectRepository.GetProjectWithConvocationByIdAsync(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        public IActionResult CreateCollaboratorsDetails(int id)
        {
            return RedirectToAction("AddCollaborator", "ProjectAssignations", new {id = id });
        }

    }
}
