using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository repository;
        private readonly IProjectRepository projectRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IPriorityRepository priorityRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ICombosHelper combosHelper;

        public ActivitiesController(IActivityRepository repository, ICombosHelper combosHelper, IProjectRepository projectRepository,
            ICategoryRepository categoryRepository, IPriorityRepository priorityRepository,
            IStatusRepository statusRepository, IStudentRepository studentRepository)
        {
            this.repository = repository;
            this.combosHelper = combosHelper;
            this.projectRepository = projectRepository;
            this.categoryRepository = categoryRepository;
            this.priorityRepository = priorityRepository;
            this.statusRepository = statusRepository;
            this.studentRepository = studentRepository;
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Index()
        {
            return View(repository.GetAllActivitiesWithProjectsCategoriesPrioritiesStatuses());
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await repository.GetActivityWithProjectCategoryPriorityStatusAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Create()
        {
            var model = new ActivityViewModel
            {
                Projects = this.combosHelper.GetComboProjects(),
                Categories = this.combosHelper.GetComboCategories(),
                Priorities = this.combosHelper.GetComboPriorities(),
                Statuses = this.combosHelper.GetComboStatuses()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var activityDetailTemps = await repository.GetAllActivityDetailTemps().ToListAsync();
                if (activityDetailTemps == null || activityDetailTemps.Count() == 0)
                    return View(model);

                var details = activityDetailTemps.Select(adt =>
                studentRepository.GetStudentWithUserGenderAndCareerById(adt.Student.Id)).ToList();

                var activity = new Activity
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Deadline = model.Deadline,
                    Progress = 0,
                    CreationDate = DateTime.Now,
                    Project = this.projectRepository.GetProjectWithConvocationAndCollaboratorsById(model.ProjectId),
                    Category = this.categoryRepository.GetDetailById(model.CategoryId),
                    Priority = this.priorityRepository.GetMasterById(model.PriorityId),
                    Status = this.statusRepository.GetMasterById(model.StatusId),
                    AssignedActivities = details
                };

                await repository.CreateActivityAsync(activity, activityDetailTemps);
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

            var activity = await repository.GetActivityWithProjectCategoryPriorityStatusAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }
            var model = new ActivityViewModel
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Deadline = activity.Deadline,
                Progress = activity.Progress,
                CreationDate = activity.CreationDate,
                Project = activity.Project,
                ProjectId = activity.Project.Id,
                Projects = this.combosHelper.GetComboProjects(),
                Category = activity.Category,
                CategoryId = activity.Category.Id,
                Categories = this.combosHelper.GetComboCategories(),
                Priority = activity.Priority,
                PriorityId = activity.Priority.Id,
                Priorities = this.combosHelper.GetComboPriorities(),
                Status = activity.Status,
                StatusId = activity.Status.Id,
                Statuses = this.combosHelper.GetComboStatuses(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ActivityViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await repository.ExistAsync(model.Id))
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

            var activity = await repository.GetActivityWithProjectCategoryPriorityStatusAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await repository.GetActivityWithProjectCategoryPriorityStatusAsync(id);
            await repository.DeleteAsync(activity);
            return RedirectToAction(nameof(Index));
        }


        //Student Methods
        public IActionResult AddStudent()
        {
            var model = new AddStudentViewModel
            {
                StudentId = -1,
                StudentsList = combosHelper.GetComboStudents(),
                AssignedStudents = repository.GetAllActivityDetailTemps()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = studentRepository.GetStudentWithUserGenderAndCareerById(model.StudentId);
                if (student == null)
                    NotFound();
                var activityDetailTemp = repository.GetActivityDetailTempByContactId(student.Id);
                if (activityDetailTemp == null)
                {
                    activityDetailTemp = new ActivityDetailTemp
                    {
                        Student = student
                    };
                    repository.AddActivityDetailTemp(activityDetailTemp);
                }
                return RedirectToAction("Create");
            }
            return View(model);
        }


        public IActionResult DeleteStudent(int? id)
        {
            if (id == null)
                return NotFound();

            var activityDetailTemp = repository.GetActivityDetailTempById(id.Value);
            if (activityDetailTemp == null)
                return NotFound();

            repository.DeleteActivityDetailTemp(activityDetailTemp);
            return RedirectToAction("AddStudent");
        }
    }
}
