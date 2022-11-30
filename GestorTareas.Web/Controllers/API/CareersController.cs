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
        public async Task<IActionResult> PutCareer([FromRoute] int id, [FromBody] CareerResponse careerResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != careerResponse.Id)
                return BadRequest();

            var oldCareer = await this.repository.GetByIdAsync(id);

            if (oldCareer == null)
                return BadRequest("The career doesn't exist");

            oldCareer.Name = careerResponse.Name;
            oldCareer.CareerCode = careerResponse.CareerCode;

            var updatedCareer = await repository.UpdateAsync(oldCareer);
            return Ok(updatedCareer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var career = await this.repository.GetByIdAsync(id);

            if (career == null)
                return BadRequest("The career doesn't exist");

            await repository.DeleteAsync(career);

            return Ok(career);
        }
    }
}
