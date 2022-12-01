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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus([FromRoute] int id, [FromBody] StatusResponse statusResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != statusResponse.Id)
                return BadRequest();

            var oldStatus = await this.repository.GetByIdAsync(id);

            if (oldStatus == null)
                return BadRequest("The status doesn't exist");

            oldStatus.StatusName = statusResponse.StatusName;

            var updatedStatus = await repository.UpdateAsync(oldStatus);
            return Ok(updatedStatus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var status = await this.repository.GetByIdAsync(id);

            if (status == null)
                return BadRequest("The status doesn't exist");

            await repository.DeleteAsync(status);

            return Ok(status);
        }
    }
}
