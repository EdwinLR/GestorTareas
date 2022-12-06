using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository repository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IPriorityRepository priorityRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IProjectRepository projectRepository;

        public ActivitiesController(IActivityRepository repository, ICategoryRepository categoryRepository,
            IPriorityRepository priorityRepository, IStatusRepository statusRepository,
            IProjectRepository projectRepository)
        {
            this.repository = repository;
            this.categoryRepository = categoryRepository;
            this.priorityRepository = priorityRepository;
            this.statusRepository = statusRepository;
            this.projectRepository = projectRepository;
        }

        [HttpGet]
        public IActionResult GetActivities()
        {
            return Ok(this.repository.GetAllActivitiesResponse());
        }

        [HttpGet("{id}")]
        public IActionResult GetActivity(int id)
        {
            var activity = this.repository.GetActivityResponseById(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        public async Task<IActionResult> PostActivity([FromBody] ActivityResponse activityResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = categoryRepository.GetCategoryByName(activityResponse.Category);
            if (category == null)
                return BadRequest("The category does not exist");

            var priority = priorityRepository.GetPriorityByName(activityResponse.Priority);
            if (priority == null)
                return BadRequest("The priority does not exist");

            var status = statusRepository.GetStatusByName(activityResponse.Status);
            if (status == null)
                return BadRequest("The status does not exist");

            var project = projectRepository.GetProjectByName(activityResponse.Project);
            if (project == null)
                return BadRequest("The project does not exist");

            var activity = new Activity
            {
                Id = activityResponse.Id,
                Title = activityResponse.Title,
                CreationDate = System.DateTime.Now,
                Description = activityResponse.Description,
                Deadline = activityResponse.Deadline,
                Progress = activityResponse.Progress,
                Category = category,
                Priority = priority,
                Status = status,
                Project = project
            };

            var newActivity = await this.repository.CreateAsync(activity);

            return Ok(newActivity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity([FromRoute] int id, [FromBody] ActivityResponse activityResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != activityResponse.Id)
                return BadRequest();

            var oldActivity = await this.repository.GetByIdAsync(id);
            if (oldActivity == null)
                return BadRequest("The activity does not exist.");

            var category = categoryRepository.GetCategoryByName(activityResponse.Category);
            if (category == null)
                return BadRequest("The category does not exist");

            var priority = priorityRepository.GetPriorityByName(activityResponse.Priority);
            if (priority == null)
                return BadRequest("The priority does not exist");

            var status = statusRepository.GetStatusByName(activityResponse.Status);
            if (status == null)
                return BadRequest("The status does not exist");

            var project = projectRepository.GetProjectByName(activityResponse.Project);
            if (project == null)
                return BadRequest("The project does not exist");

            oldActivity.Id = activityResponse.Id;
            oldActivity.Title = activityResponse.Title;
            oldActivity.CreationDate = activityResponse.CreationDate;
            oldActivity.Description = activityResponse.Description;
            oldActivity.Deadline = activityResponse.Deadline;
            oldActivity.Progress = activityResponse.Progress;
            oldActivity.Category = category;
            oldActivity.Priority = priority;
            oldActivity.Status = status;
            oldActivity.Project = project;

            var updatedActivity = await this.repository.UpdateAsync(oldActivity);

            return Ok(updatedActivity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var activity = await this.repository.GetByIdAsync(id);
            if (activity == null)
                return BadRequest("Activity does not exist");

            await repository.DeleteAsync(activity);
            return Ok(activity);
        }
    }
}
