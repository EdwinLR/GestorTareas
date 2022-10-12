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
    public class CoordinatorsController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;

        public CoordinatorsController(DataContext dataContext,
            IImageHelper imageHelper, IUserHelper userHelper, ICombosHelper combosHelper)
        {
            this.dataContext = dataContext;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await dataContext.Coordinators
                .Include(u => u.User)
                .ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await dataContext.Coordinators
                .Include(u => u.User)
                .Include(g => g.Gender)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (coordinator == null)
            {
                return NotFound();
            }

            return View(coordinator);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new CoordinatorViewModel
            {
                Genders = this.combosHelper.GetComboGenders()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoordinatorViewModel model)
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
                        PhotoUrl = await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName,"Coordinators"),
                    };
                    var result = await userHelper.AddUserAsync(user, model.WorkerId.ToString() + model.User.FatherLastName[0] + model.User.MotherLastName[0] + model.User.FirstName[0] + model.User.FirstName[1]);
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("ERROR. No se pudo crear el usuario.");
                    }
                    await userHelper.AddUserToRoleAsync(user, "Coordinator");
                }

                var coordinator = new Coordinator
                {
                    WorkerId = model.WorkerId,
                    Gender = await this.dataContext.Genders.FindAsync(model.GenderId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };
                dataContext.Add(coordinator);
                await dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await dataContext.Coordinators
                .Include(u => u.User)
                .Include(g => g.Gender)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (coordinator == null)
            {
                return NotFound();
            }

            var model = new CoordinatorViewModel
            {
                Id = coordinator.Id,
                WorkerId = coordinator.WorkerId,
                User = coordinator.User,
                Gender = coordinator.Gender,
                GenderId = coordinator.Gender.Id,
                Genders = this.combosHelper.GetComboGenders()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CoordinatorViewModel model)
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
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Coordinators") :
                        await imageHelper.UpdateImageAsync(model.ImageFile, model.User.PhotoUrl)) :
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Coordinators"));

                this.dataContext.Update(user);
                await dataContext.SaveChangesAsync();

                var coordinator = new Coordinator
                {
                    Id = model.Id,
                    WorkerId = model.WorkerId,
                    Gender = await this.dataContext.Genders.FindAsync(model.GenderId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };

                this.dataContext.Update(coordinator);
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

            var coordinator = await dataContext.Coordinators
                .Include(u => u.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (coordinator == null)
            {
                return NotFound();
            }

            return View(coordinator);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordinator = await dataContext.Coordinators
                .Include(u => u.User)
                .FirstOrDefaultAsync(t => t.Id == id);


            dataContext.Coordinators.Remove(coordinator);

            var user = await dataContext.Users
                .FindAsync(coordinator.User.Id);
            dataContext.Users.Remove(user);

            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
