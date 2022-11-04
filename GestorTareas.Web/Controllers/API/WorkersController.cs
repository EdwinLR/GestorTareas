using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class WorkersController : Controller
    {
        private readonly IWorkerRepository repository;

        public WorkersController(IWorkerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetWorkers()
        {
            return Ok(this.repository.GetAllWorkersWithUserAndPositionOrderByPosition());
        }
    }
}
