using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender([FromRoute] int id, [FromBody] GenderResponse genderResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != genderResponse.Id)
                return BadRequest();

            var oldGender = await this.repository.GetByIdAsync(id);
            if (oldGender == null)
                return BadRequest("The gender does not exist.");

            oldGender.GenderName = genderResponse.GenderName;

            var updatedGender = await this.repository.UpdateAsync(oldGender);

            return Ok(updatedGender);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gender = await this.repository.GetGenderWithStudentsById(id);
            if (gender == null)
                return BadRequest("Gender does not exist");

            if (gender.Students == null || gender.Students.Count != 0)
                return BadRequest("The gender cannot be deleted. It is linked with one or more students");

            await repository.DeleteAsync(gender);
            return Ok(gender);
        }
    }
}
