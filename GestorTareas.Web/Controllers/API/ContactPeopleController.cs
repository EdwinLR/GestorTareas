using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPeopleController : ControllerBase
    {
        private readonly IContactPersonRepository repository;

        public ContactPeopleController(IContactPersonRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetContactPeople()
        {
            return Ok(this.repository.GetAllContactPeopleResponses());
        }

        [HttpGet("{id}")]
        public IActionResult GetContactPerson(int id)
        {
            var contactPerson = this.repository.GetContactPersonResponseById(id);

            if (contactPerson == null)
            {
                return NotFound();
            }

            return Ok(contactPerson);
        }
    }
}
