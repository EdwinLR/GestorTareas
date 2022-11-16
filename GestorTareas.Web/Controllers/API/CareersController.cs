namespace GestorTareas.Web.Controllers.API
{
    using GestorTareas.Web.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class CareersController : Controller
    {
        private readonly ICareerRepository repository;

        public CareersController(ICareerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetCareers()
        {
            return Ok(this.repository.GetAllICareerWithStudents());
        }

        //[HttpPost]
        //public async Task<IActionResult> PostCareer([FromBody ])
        //{

        //}
    }
}
