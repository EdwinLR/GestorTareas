using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvocationsController : ControllerBase
    {
        private readonly IConvocationRepository repository;
        private readonly IInstituteRepository instituteRepository;

        public ConvocationsController(IConvocationRepository repository, IInstituteRepository instituteRepository)
        {
            this.repository = repository;
            this.instituteRepository = instituteRepository;
        }

        [HttpGet]
        public IActionResult GetConvocations()
        {
            return Ok(this.repository.GetAllConvocationsResponses());
        }

        [HttpGet("{id}")]
        public IActionResult GetConvocation(int id)
        {
            var convocation = this.repository.GetConvocationResponseById(id);

            if (convocation == null)
            {
                return NotFound();
            }

            return Ok(convocation);
        }

        [HttpPost]
        public async Task<IActionResult> PostConvocation([FromBody] ConvocationResponse convocationResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institute = await this.instituteRepository.GetInstituteByName(convocationResponse.Institute);

            if (institute == null)
                return BadRequest("The Institute you're trying to get doesn't exist");

            var convocation = new Convocation
            {
                Id = convocationResponse.Id,
                Summary = convocationResponse.Summary,
                StartingDate = convocationResponse.StartingDate,
                EndingDate = convocationResponse.EndingDate,
                Requirements = convocationResponse.Requirements,
                Prizes = convocationResponse.Prizes,
                ConvocationUrl = convocationResponse.ConvocationUrl,
                Institute = institute
            };

            var newConvocation = await this.repository.CreateAsync(convocation);

            return Ok(newConvocation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConvocation([FromRoute] int id, [FromBody] ConvocationResponse convocationResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != convocationResponse.Id)
                return BadRequest();

            var newInstitute = await this.instituteRepository.GetInstituteByName(convocationResponse.Institute);

            if (newInstitute == null)
                return BadRequest("The Institute you're trying to get doesn't exist");

            var oldConvocation = await this.repository.GetByIdAsync(id);

            if (oldConvocation == null)
                return BadRequest("The Convocation doesn't exist");

            oldConvocation.Id = convocationResponse.Id;
            oldConvocation.Summary = convocationResponse.Summary;
            oldConvocation.StartingDate = convocationResponse.StartingDate;
            oldConvocation.EndingDate = convocationResponse.EndingDate;
            oldConvocation.Requirements = convocationResponse.Requirements;
            oldConvocation.Prizes = convocationResponse.Prizes;
            oldConvocation.ConvocationUrl = convocationResponse.ConvocationUrl;
            oldConvocation.Institute = newInstitute;

            var updatedConvocation = await repository.UpdateAsync(oldConvocation);
            return Ok(updatedConvocation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var convocation = await this.repository.GetByIdAsync(id);

            if (convocation == null)
                return BadRequest("The Convocation doesn't exist");

            await repository.DeleteAsync(convocation);

            return Ok(convocation);
        }
    }
}
