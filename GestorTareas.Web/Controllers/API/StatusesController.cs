using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var status = this.repository.GetMasterById(id);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> PostStatus([FromBody] StatusResponse statusResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = new Status
            {
                Id = statusResponse.Id,
                StatusName = statusResponse.StatusName
            };

            var newStatus = await this.repository.CreateAsync(status);

            return Ok(newStatus);
        }
    }
}
