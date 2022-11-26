using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository repository;

        public StudentsController(IStudentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(this.repository.GetAllStudentsWithUserGenderAndCareer());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = this.repository.GetStudentWithUserGenderAndCareerById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
    }
}
