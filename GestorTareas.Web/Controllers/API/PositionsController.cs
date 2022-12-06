using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionRepository repository;

        public PositionsController(IPositionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetPositions()
        {
            return Ok(this.repository.GetAllPositionsResponsesWithWorkers());
        }

        [HttpGet("{id}")]
        public IActionResult GetPosition(int id)
        {
            var position = this.repository.GetPositionResponseById(id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> PostPosition([FromBody] PositionResponse positionResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = new Position
            {
                Id = positionResponse.Id,
                Description = positionResponse.Description
            };

            var newPosition = await this.repository.CreateAsync(position);

            return Ok(newPosition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition([FromRoute] int id, [FromBody] PositionResponse positionResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != positionResponse.Id)
                return BadRequest();

            var oldPosition = await this.repository.GetByIdAsync(id);

            if (oldPosition == null)
                return BadRequest("The position doesn't exist");

            oldPosition.Description = positionResponse.Description;

            var updatedPosition = await repository.UpdateAsync(oldPosition);
            return Ok(updatedPosition);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var position = this.repository.GetPositionWithWorkersById(id);

            if (position == null)
                return BadRequest("The position doesn't exist");

            if (position.Workers.Count != 0)
                return BadRequest("The Position cannot be deleted. It is linked with one or more workers");

            await repository.DeleteAsync(position);

            return Ok(position);
        }
    }
}
