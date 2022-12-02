using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository repository;
        private readonly ICareerRepository careerRepository;
        private readonly IGenderRepository genderRepository;
        private readonly IUserHelper userHelper;

        public StudentsController(IStudentRepository repository, ICareerRepository careerRepository,
            IGenderRepository genderRepository, IUserHelper userHelper)
        {
            this.repository = repository;
            this.careerRepository = careerRepository;
            this.genderRepository = genderRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(this.repository.GetAllStudentsResponseWithGenderAndCareer());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = this.repository.GetStudentResponseById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] StudentResponse studentResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByIdAsync(studentResponse.UserId);
            if (user == null)
            {
                user = new User
                {
                    FirstName = studentResponse.FirstName,
                    FatherLastName = studentResponse.FatherLastName,
                    MotherLastName = studentResponse.MotherLastName,
                    Email = studentResponse.Email
                };
                string password = studentResponse.StudentId + studentResponse.FatherLastName[0] + studentResponse.MotherLastName[0] + studentResponse.FirstName[0] + studentResponse.FirstName[1];
                var result = await userHelper.AddUserAsync(user, password.ToUpper());
                if (result != IdentityResult.Success)
                    return BadRequest();

                var career = careerRepository.GetCareerByName(studentResponse.Career);
                if (career == null)
                    return BadRequest("The career does not exit");

                var gender = genderRepository.GetGenderByName(studentResponse.Gender);
                if (gender == null)
                    return BadRequest("The gender does not exist");

                var student = new Student
                {
                    Id = studentResponse.Id,
                    StudentId = studentResponse.StudentId,
                    Career = career,
                    Gender = gender,
                    User = user
                };

                var newStudent = await this.repository.CreateAsync(student);
                return Ok(newStudent);
            }
            else
            {
                var worker = repository.GetStudentWithUserGenderAndCareerByUserId(user.Id);
                if (worker == null)
                    return BadRequest("This worker already exists");
                else
                {
                    var career = careerRepository.GetCareerByName(studentResponse.Career);
                    if (career == null)
                        return BadRequest("The career does not exit");

                    var gender = genderRepository.GetGenderByName(studentResponse.Gender);
                    if (gender == null)
                        return BadRequest("The gender does not exist");

                    var student = new Student
                    {
                        Id = studentResponse.Id,
                        StudentId = studentResponse.StudentId,
                        Career = career,
                        Gender = gender,
                        User = user
                    };

                    var newStudent = await this.repository.CreateAsync(student);
                    return Ok(newStudent);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] StudentResponse studentResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != studentResponse.Id)
                return BadRequest();

            var oldStudent = await this.repository.GetByIdAsync(id);

            if (oldStudent == null)
                return BadRequest("The student doesn't exist");

            var career = careerRepository.GetCareerByName(studentResponse.Career);
            if (career == null)
                return BadRequest("The career does not exit");

            var gender = genderRepository.GetGenderByName(studentResponse.Gender);
            if (gender == null)
                return BadRequest("The gender does not exist");

            var user = await userHelper.GetUserByIdAsync(studentResponse.UserId);
            if (user == null)
                return BadRequest("The user does not exist");

            user.FirstName = studentResponse.FirstName;
            user.FatherLastName = studentResponse.FatherLastName;
            user.MotherLastName = studentResponse.MotherLastName;
            user.Email = studentResponse.Email;

            oldStudent.Id = studentResponse.Id;
            oldStudent.StudentId = studentResponse.StudentId;
            oldStudent.Career = career;
            oldStudent.Gender = gender;
            oldStudent.User = user;

            var result = await userHelper.UpdateUserAsync(user);
            if (result != IdentityResult.Success)
                return BadRequest();
            var updatedStudent = await repository.UpdateAsync(oldStudent);
            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await this.repository.GetByIdAsync(id);

            if (student == null)
                return BadRequest("The student doesn't exist");

            await repository.DeleteAsync(student);

            return Ok(student);
        }
    }
}
