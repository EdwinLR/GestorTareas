using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusRepository repository;

        public StatusesController(IStatusRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetStatuses()
        {
            return Ok(this.repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetStatus(int id)
        {
            var status = this.repository.GetMasterByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }
    }
}
