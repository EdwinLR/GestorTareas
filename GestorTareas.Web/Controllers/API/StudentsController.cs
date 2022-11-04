using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository repository;

        public StudentsController(IStudentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(this.repository.GetAll());
        }
    }
}
