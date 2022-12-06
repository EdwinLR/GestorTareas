using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : ControllerBase
    {
        private readonly IInstituteRepository repository;
        private readonly ICountryRepository countryRepository;

        public InstitutesController(IInstituteRepository repository, ICountryRepository countryRepository)
        {
            this.repository = repository;
            this.countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetInstitutes()
        {
            return Ok(this.repository.GetAllInstitutesResponses());
        }

        [HttpGet("{id}")]
        public IActionResult GetInstitute(int id)
        {
            var institute = this.repository.GetInstituteResponseById(id);

            if (institute == null)
            {
                return NotFound();
            }

            return Ok(institute);
        }

        [HttpPost]
        public async Task<IActionResult> PostInstitute([FromBody] InstituteResponse instituteResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = await this.countryRepository.GetCountryByName(instituteResponse.Country);

            if (country == null)
                return BadRequest("The Country you're trying to get doesn't exist");

            var institute = new Institute
            {
                Id = instituteResponse.Id,
                Name = instituteResponse.Name,
                City = instituteResponse.City,
                StreetName = instituteResponse.StreetName,
                District = instituteResponse.District,
                ContactPhone = instituteResponse.ContactPhone,
                StreetNumber = instituteResponse.StreetNumber,
                Country = country

            };

            var newInstitute = await this.repository.CreateAsync(institute);

            return Ok(newInstitute);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitute([FromRoute] int id, [FromBody] InstituteResponse instituteResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != instituteResponse.Id)
                return BadRequest();

            var newCountry = await this.countryRepository.GetCountryByName(instituteResponse.Country);

            if (newCountry == null)
                return BadRequest("The Country you're trying to get doesn't exist");

            var oldInstitute = await this.repository.GetByIdAsync(id);

            if (oldInstitute == null)
                return BadRequest("The Institute doesn't exist");

            oldInstitute.Name = instituteResponse.Name;
            oldInstitute.ContactPhone = instituteResponse.ContactPhone;
            oldInstitute.Country = newCountry;
            oldInstitute.City = instituteResponse.City;
            oldInstitute.StreetName = instituteResponse.StreetName;
            oldInstitute.StreetNumber = instituteResponse.StreetNumber;
            oldInstitute.City = instituteResponse.City;
            oldInstitute.District = instituteResponse.District;

            var updatedInstitute = await repository.UpdateAsync(oldInstitute);
            return Ok(updatedInstitute);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var institute = this.repository.GetInstituteWithCountryAndContactPersonById(id);

            if (institute == null)
                return BadRequest("The institute doesn't exist");

            if (institute.ContactPeople.Count != 0 || institute.Convocations.Count != 0)
                return BadRequest("The institute cannot be deleted. It is linked with other entities");

            await repository.DeleteAsync(institute);

            return Ok(institute);
        }
    }
}
