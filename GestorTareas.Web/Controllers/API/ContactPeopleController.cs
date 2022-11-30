using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPeopleController : ControllerBase
    {
        private readonly IContactPersonRepository repository;
        private readonly IInstituteRepository instituteRepository;

        public ContactPeopleController(IContactPersonRepository repository, IInstituteRepository instituteRepository)
        {
            this.repository = repository;
            this.instituteRepository = instituteRepository;
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

        [HttpPost]
        public async Task<IActionResult> PostContactPerson([FromBody] ContactPersonResponse contactPersonResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institute = await this.instituteRepository.GetInstituteByName(contactPersonResponse.Institute);

            if (institute == null)
                return BadRequest("The Institute you're trying to get doesn't exist");

            var contactPerson = new ContactPerson
            {
                Id = contactPersonResponse.Id,
                FirstName = contactPersonResponse.FirstName,
                FatherLastName = contactPersonResponse.FatherLastName,
                MotherLastName = contactPersonResponse.MotherLastName,
                PhoneNumber = contactPersonResponse.PhoneNumber,
                Email = contactPersonResponse.Email,
                Institute = institute

            };

            var newContactPerson = await this.repository.CreateAsync(contactPerson);

            return Ok(newContactPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitute([FromRoute] int id, [FromBody] ContactPersonResponse contactPersonResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != contactPersonResponse.Id)
                return BadRequest();

            var newInstitute = await this.instituteRepository.GetInstituteByName(contactPersonResponse.Institute);

            if (newInstitute == null)
                return BadRequest("The Institute you're trying to get doesn't exist");

            var oldContactPerson = await this.repository.GetByIdAsync(id);

            if (oldContactPerson == null)
                return BadRequest("The Contact Person doesn't exist");

            oldContactPerson.Id = contactPersonResponse.Id;
            oldContactPerson.FirstName = contactPersonResponse.FirstName;
            oldContactPerson.FatherLastName = contactPersonResponse.FatherLastName;
            oldContactPerson.MotherLastName = contactPersonResponse.MotherLastName;
            oldContactPerson.PhoneNumber = contactPersonResponse.PhoneNumber;
            oldContactPerson.Email = contactPersonResponse.Email;
            oldContactPerson.Institute = newInstitute;

            var updatedContactPerson = await repository.UpdateAsync(oldContactPerson);
            return Ok(updatedContactPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactPerson = await this.repository.GetByIdAsync(id);

            if (contactPerson == null)
                return BadRequest("The Contact Person doesn't exist");

            await repository.DeleteAsync(contactPerson);

            return Ok(contactPerson);
        }
    }
}
