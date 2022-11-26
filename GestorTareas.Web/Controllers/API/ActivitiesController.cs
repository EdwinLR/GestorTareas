using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository repository;

        public ActivitiesController(IActivityRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetActivities()
        {
            return Ok(this.repository.GetAllActivitiesWithProjectsCategoriesPrioritiesStatuses());
        }

        [HttpGet("{id}")]
        public IActionResult GetActivity(int id)
        {
            var activity = this.repository.GetActivityWithProjectCategoryPriorityStatusAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }
    }
}
