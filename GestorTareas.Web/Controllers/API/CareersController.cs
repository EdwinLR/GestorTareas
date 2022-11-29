namespace GestorTareas.Web.Controllers.API
{
    using GestorTareas.Common.Models;
    using GestorTareas.Web.Data.Entities;
    using GestorTareas.Web.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class CareersController : ControllerBase
    {
        private readonly ICareerRepository repository;

        public CareersController(ICareerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetCareers()
        {
            return Ok(this.repository.GetAllCareersResponseWithStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetCareer(int id)
        {
            var career = this.repository.GetCareerResponseById(id);

            if (career == null)
            {
                return NotFound();
            }

            return Ok(career);
        }

        [HttpPost]
        public async Task<IActionResult> PostCareer(CareerResponse careerResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var career = new Career
            {
                Id = careerResponse.Id,
                Name = careerResponse.Name,
                CareerCode = careerResponse.CareerCode
            };

            var newCareer = await repository.CreateAsync(career);
            return Ok(newCareer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCareer(int id, Career career)
        {
            if (id != career.Id)
            {
                return BadRequest();
            }

            if (!CareerExists(id).Result)
                return NoContent();

            var updatedCareer = await repository.UpdateAsync(career);
            return Ok(updatedCareer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Career>> DeleteCareer(int id)
        {
            var career = this.repository.GetCareerById(id);
            if (career == null)
            {
                return NotFound();
            }

            await repository.DeleteAsync(career);

            return career;
        }

        private async Task<bool> CareerExists(int id)
        {
            return await repository.ExistAsync(id);
        }
    }
}
