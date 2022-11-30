using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        //public async Task<IActionResult> PostPosition([FromBody] InstituteResponse instituteResponse)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var institute = new Institute
        //    {
        //        Id = instituteResponse.Id,
        //        Name= instituteResponse.Name,
        //        City= instituteResponse.City,
        //        StreetName = instituteResponse.StreetName,
        //        District= instituteResponse.District,
        //        ContactPhone = instituteResponse.ContactPhone,
        //        StreetNumber = instituteResponse.StreetNumber
                
        //    };

        //    var newInstitute = await this.repository.CreateAsync(institute);

        //    return Ok(newInstitute);
        //}
    }
}
