using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : ControllerBase
    {
        private readonly IInstituteRepository repository;

        public InstitutesController(IInstituteRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetInsitutes()
        {
            return Ok(this.repository.GetAllInstitutesWithCountriesAndContactPeople());
        }

        [HttpGet("{id}")]
        public IActionResult GetInsitute(int id)
        {
            var insitute = this.repository.GetInstituteWithCountryAndContactPersonById(id);

            if (insitute == null)
            {
                return NotFound();
            }

            return Ok(insitute);
        }
    }
}
