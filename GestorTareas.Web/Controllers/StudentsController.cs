using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository repository;
        private readonly ICareerRepository careerRepository;
        private readonly IGenderRepository genderRepository;
        private readonly DataContext dataContext;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;

        public StudentsController(IStudentRepository repository, DataContext dataContext,
            ICareerRepository careerRepository, IGenderRepository genderRepository,
            IImageHelper imageHelper, IUserHelper userHelper, ICombosHelper combosHelper)
        {
            this.repository = repository;
            this.careerRepository = careerRepository;
            this.genderRepository = genderRepository;
            this.dataContext = dataContext;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Coordinator,Admin,Teacher")]
        public IActionResult Index()
        {
            return View(repository.GetAllStudentsWithUserAndCareerOrderByFatherLastname());
        }

        [Authorize(Roles = "Coordinator,Admin,Teacher")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = repository.GetStudentWithUserAndCareerById(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new StudentViewModel
            {
                Careers = this.combosHelper.GetComboCareers(),
                Genders = this.combosHelper.GetComboGenders()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByIdAsync(model.User.Id);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.User.FirstName,
                        FatherLastName = model.User.FatherLastName,
                        MotherLastName = model.User.MotherLastName,
                        PhoneNumber = model.User.PhoneNumber,
                        Email = model.User.Email,
                        UserName = model.User.Email,
                        PhotoUrl = await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Students"),
                    };
                    string password = model.StudentId.ToString() + model.User.FatherLastName[0] + model.User.MotherLastName[0] + model.User.FirstName[0] + model.User.FirstName[1];
                    var result = await userHelper.AddUserAsync(user, password.ToUpper());
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("ERROR. No se pudo crear el usuario.");
                    }
                    await userHelper.AddUserToRoleAsync(user, "Student");
                }

                var student = new Student
                {
                    StudentId = model.StudentId,
                    Career = this.careerRepository.GetCareerById(model.CareerId),
                    Gender = this.genderRepository.GetGenderById(model.GenderId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };
                await repository.CreateAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = repository.GetStudentWithUserAndCareerById(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentViewModel
            {
                Id = student.Id,
                StudentId = student.StudentId,
                User = student.User,
                Career = student.Career,
                CareerId = student.Career.Id,
                Careers = this.combosHelper.GetComboCareers(),
                Gender = student.Gender,
                GenderId = student.Gender.Id,
                Genders = this.combosHelper.GetComboGenders()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.dataContext.Users.FindAsync(model.User.Id);
                user.FirstName = model.User.FirstName;
                user.FatherLastName = model.User.FatherLastName;
                user.MotherLastName = model.User.MotherLastName;
                user.PhoneNumber = model.User.PhoneNumber;
                user.Email = model.User.Email;
                user.UserName = model.User.Email;
                user.PhotoUrl = (model.User.PhotoUrl != null ?
                        (model.User.PhotoUrl.Contains("_default.png") ?
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Students") :
                        await imageHelper.UpdateImageAsync(model.ImageFile, model.User.PhotoUrl)) :
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Students"));

                this.dataContext.Update(user);
                await dataContext.SaveChangesAsync();

                var student = new Student
                {
                    Id = model.Id,
                    StudentId = model.StudentId,
                    Career = this.careerRepository.GetCareerById(model.CareerId),
                    Gender = this.genderRepository.GetGenderById(model.GenderId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };

                await repository.UpdateAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = repository.GetStudentWithUserAndCareerById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = repository.GetStudentWithUserAndCareerById(id);
            await repository.DeleteStudentAndUserAsync(student);
            return RedirectToAction(nameof(Index));
        }
    }
}
