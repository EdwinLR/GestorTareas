using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IGenderRepository repository;

        public GendersController(IGenderRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetGenders()
        {
            return Ok(this.repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetGender(int id)
        {
            var gender = this.repository.GetGenderById(id);

            if (gender == null)
            {
                return NotFound();
            }

            return Ok(gender);
        }
    }
}
