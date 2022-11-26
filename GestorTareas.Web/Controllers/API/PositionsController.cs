using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(this.repository.GetAllPositionsWithWorkers());
        }

        [HttpGet("{id}")]
        public IActionResult GetPosition(int id)
        {
            var position = this.repository.GetPositionWithWorkersById(id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }
    }
}
