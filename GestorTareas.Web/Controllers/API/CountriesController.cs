using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository repository;

        public CountriesController(ICountryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(this.repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCountry(int id)
        {
            var country = this.repository.GetMasterById(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> PostCountry([FromBody] CountryResponse countryResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = new Country
            {
                Id = countryResponse.Id,
                CountryName = countryResponse.CountryName
            };

            var newCountry = await this.repository.CreateAsync(country);

            return Ok(newCountry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry([FromRoute] int id, [FromBody] CountryResponse countryResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != countryResponse.Id)
                return BadRequest();

            var oldCountry = await this.repository.GetByIdAsync(id);

            if (oldCountry == null)
                return BadRequest("The country doesn't exist");

            oldCountry.CountryName = countryResponse.CountryName;

            var updatedCountry = await repository.UpdateAsync(oldCountry);
            return Ok(updatedCountry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = await this.repository.GetByIdAsync(id);

            if (country == null)
                return BadRequest("The country doesn't exist");

            await repository.DeleteAsync(country);

            return Ok(country);
        }
    }
}
