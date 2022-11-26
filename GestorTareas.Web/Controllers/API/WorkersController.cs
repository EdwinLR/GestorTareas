using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerRepository repository;

        public WorkersController(IWorkerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetWorkers()
        {
            return Ok(this.repository.GetAllWorkersWithUserAndPositionOrderByFatherLastname());
        }

        [HttpGet("{id}")]
        public IActionResult GetWorker(int id)
        {
            var worker = this.repository.GetWorkerWithUserAndPositionById(id);

            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }
    }
}
