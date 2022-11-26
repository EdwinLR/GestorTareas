using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvocationsController : ControllerBase
    {
        private readonly IConvocationRepository repository;

        public ConvocationsController(IConvocationRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetConvocations()
        {
            return Ok(this.repository.GetAllConvocationsWithInstitutesCountriesAndContactPeople());
        }

        [HttpGet("{id}")]
        public IActionResult GetConvocation(int id)
        {
            var convocation = this.repository.GetConvocationWithInstituteCountryAndContactPersonAsync(id);

            if (convocation == null)
            {
                return NotFound();
            }

            return Ok(convocation);
        }
    }
}
