using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPeopleController : ControllerBase
    {
        private readonly IInstituteRepository repository;

        public ContactPeopleController(IInstituteRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetContactPeople()
        {
            return Ok(this.repository.GetAllContactPeople());
        }

        [HttpGet("{id}")]
        public IActionResult GetContactPerson(int id)
        {
            var contactPerson = this.repository.GetContactPersonById(id);

            if (contactPerson == null)
            {
                return NotFound();
            }

            return Ok(contactPerson);
        }
    }
}
