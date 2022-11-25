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
        private readonly ICountryRepository countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(countryRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCountry(int id)
        {
            var country = countryRepository.GetDetailByIdAsync(id);

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

            var newCountry = await this.countryRepository.CreateAsync(country);

            return Ok(newCountry);
        }
    }
}
