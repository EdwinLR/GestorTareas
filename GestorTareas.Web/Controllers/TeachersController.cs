using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;

        public TeachersController(DataContext dataContext,
            IImageHelper imageHelper, IUserHelper userHelper, ICombosHelper combosHelper)
        {
            this.dataContext = dataContext;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await dataContext.Teachers
                .Include(u => u.User)
                .ToListAsync());
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await dataContext.Teachers
                .Include(u => u.User)
                .Include(g => g.Gender)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new TeacherViewModel
            {
                Genders = this.combosHelper.GetComboGenders()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model)
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
                        Email = model.User.Email,
                        UserName = model.User.Email,
                        PhotoUrl = await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Teachers"),
                    };
                    var result = await userHelper.AddUserAsync(user, model.WorkerId.ToString() + model.User.FatherLastName[0] + model.User.MotherLastName[0] + model.User.FirstName[0] + model.User.FirstName[1]);
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("ERROR. No se pudo crear el usuario.");
                    }
                    await userHelper.AddUserToRoleAsync(user, "Teacher");
                }

                var teacher = new Teacher
                {
                    WorkerId = model.WorkerId,
                    Gender = await this.dataContext.Genders.FindAsync(model.GenderId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };
                dataContext.Add(teacher);
                await dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await dataContext.Teachers
                .Include(u => u.User)
                .Include(g => g.Gender)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new TeacherViewModel
            {
                Id = teacher.Id,
                WorkerId = teacher.WorkerId,
                User = teacher.User,
                Gender = teacher.Gender,
                GenderId = teacher.Gender.Id,
                Genders = this.combosHelper.GetComboGenders()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.dataContext.Users.FindAsync(model.User.Id);
                user.FirstName = model.User.FirstName;
                user.FatherLastName = model.User.FatherLastName;
                user.MotherLastName = model.User.MotherLastName;
                user.Email = model.User.Email;
                user.UserName = model.User.Email;
                user.PhotoUrl = (model.User.PhotoUrl != null ?
                        (model.User.PhotoUrl.Contains("_default.png") ?
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Teachers") :
                        await imageHelper.UpdateImageAsync(model.ImageFile, model.User.PhotoUrl)) :
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Teachers"));

                this.dataContext.Update(user);
                await dataContext.SaveChangesAsync();

                var teacher = new Teacher
                {
                    Id = model.Id,
                    WorkerId = model.WorkerId,
                    Gender = await this.dataContext.Genders.FindAsync(model.GenderId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };

                this.dataContext.Update(teacher);
                await dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await dataContext.Teachers
                .Include(u => u.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await dataContext.Teachers
                .Include(u => u.User)
                .FirstOrDefaultAsync(t => t.Id == id);


            dataContext.Teachers.Remove(teacher);

            var user = await dataContext.Users
                .FindAsync(teacher.User.Id);
            dataContext.Users.Remove(user);

            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
