using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
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
    }
}
