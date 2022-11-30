using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            return Ok(this.repository.GetAllGendersResponsesWithStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetGender(int id)
        {
            var gender = this.repository.GetGenderResponseWithStudentsById(id);

            if (gender == null)
            {
                return NotFound();
            }

            return Ok(gender);
        }

        [HttpPost]
        public async Task<IActionResult> PostGender([FromBody] GenderResponse genderResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gender = new Gender
            {
                Id = genderResponse.Id,
                GenderName = genderResponse.GenderName
            };

            var newGender = await this.repository.CreateAsync(gender);

            return Ok(newGender);
        }
    }
}
