using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ICombosHelper combosHelper;
        private readonly DataContext context;
        private readonly IProjectRepository projectRepository;
        private readonly IConvocationRepository convocationRepository;

        public ProjectsController(ICombosHelper combosHelper, DataContext context, IProjectRepository projectRepository, IConvocationRepository convocationRepository)
        {
            this.combosHelper = combosHelper;
            this.context = context;
            this.projectRepository = projectRepository;
            this.convocationRepository = convocationRepository;
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
                    Convocation = this.convocationRepository.GetConvocationById(model.ConvocationId)
                };

                await this.projectRepository.CreateAsync(project);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = projectRepository.GetProjectWithConvocationAndCollaboratorsById(id.Value);

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
                    Convocation = this.convocationRepository.GetConvocationById(model.ConvocationId),
                };

                await projectRepository.UpdateAsync(project);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = projectRepository.GetProjectWithConvocationAndCollaboratorsById(id.Value);

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
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = projectRepository.GetProjectWithConvocationAndCollaboratorsById(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }


        //Métodos para agregar colaboradores
        public IActionResult AddCollaborator(int id)
        {
            var model = new AddCollaboratorViewModel
            {
                UserId = "",
                Users = combosHelper.GetComboUsers(),
                AssignedUsers = projectRepository.GetAllProjectCollaboratorsDetailTemps(),
                ProjectId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCollaborator(AddCollaboratorViewModel model)
        {
            if (this.ModelState.IsValid)
            {

                var user = await this.context.Users.FindAsync(model.UserId);
                var project = this.projectRepository.GetProjectWithConvocationAndCollaboratorsById(model.ProjectId);

                if (user == null)
                {
                    return NotFound();
                }

                var projectCollaboratorsTemp = await this.context.ProjectCollaboratorsDetailTemps
                    .Where(odt => odt.User == user && odt.Project == project).FirstOrDefaultAsync();

                if (projectCollaboratorsTemp == null)
                {
                    var projectCollaborator = this.projectRepository.GetProjectCollaboratorsById(model.ProjectId, model.UserId);

                    if (projectCollaborator == null)
                    {
                        projectCollaboratorsTemp = new ProjectCollaboratorsDetailTemp
                        {
                            Project = project,
                            User = user
                        };

                        await this.projectRepository.AddProjectCollaboratorDetailTemp(projectCollaboratorsTemp);
                    }
                    else
                    {
                        return RedirectToAction("AddCollaborator");
                    }
                }
                return RedirectToAction("AddCollaborator");
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmCollaborators(int id)
        {
            var projectCollaboratorsTemp = await this.projectRepository.GetAllProjectCollaboratorsDetailTemps().ToListAsync();

            if (projectCollaboratorsTemp == null || projectCollaboratorsTemp.Count == 0)
                return NotFound();

            var details = projectCollaboratorsTemp.Select(pct => new ProjectCollaborator
            {
                Project = pct.Project,
                User = pct.User

            }).ToList();

            await this.projectRepository.AddCollaboratorsAsync(id, details, projectCollaboratorsTemp);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCollaborator(int? id)
        {
            if (id == null)
                return NotFound();

            var collaboratorDetailTemp = projectRepository.GetProjectCollaboratorsDetailTempsById(id.Value);
            if (collaboratorDetailTemp == null)
                return NotFound();

            this.projectRepository.DeleteCollaboratorDetailTemp(collaboratorDetailTemp);
            return RedirectToAction("AddCollaborator");
        }

        public IActionResult DeleteCollaboratorFromList(int projectId, string userId)
        {
            var projectCollaborators = this.projectRepository.GetProjectCollaboratorsById(projectId, userId);

            if (projectCollaborators == null)
                return NotFound();

            this.projectRepository.DeleteCollaboratorFromList(projectCollaborators);

            return RedirectToAction("Details", new { id = projectId });
        }


    }
}
