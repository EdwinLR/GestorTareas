using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class WorkersController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;

        public WorkersController(DataContext dataContext,
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
            return View(await dataContext.Workers
                .Include(u => u.User)
                .Include(p => p.Position)
                .OrderBy(p => p.Position.Description)
                .ToListAsync());
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await dataContext.Workers
                .Include(u => u.User)
                .Include(g => g.Gender)
                .Include(p=>p.Position)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new WorkerViewModel
            {
                Genders = this.combosHelper.GetComboGenders(),
                Positions = this.combosHelper.GetComboPositions()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkerViewModel model)
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
                        PhotoUrl = await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Workers"),
                    };
                    var result = await userHelper.AddUserAsync(user, model.WorkerId.ToString() + model.User.FatherLastName[0] + model.User.MotherLastName[0] + model.User.FirstName[0] + model.User.FirstName[1]);
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("ERROR. No se pudo crear el usuario.");
                    }

                }

                var worker = new Worker
                {
                    WorkerId = model.WorkerId,
                    Gender = await this.dataContext.Genders.FindAsync(model.GenderId),
                    Position= await this.dataContext.Positions.FindAsync(model.PositionId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };

                if (worker.Position.Description!="Coordinador")
                    await userHelper.AddUserToRoleAsync(user, "Worker");
                else
                    await userHelper.AddUserToRoleAsync(user, "Coordinator");

                dataContext.Add(worker);
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

            var worker = await dataContext.Workers
                .Include(u => u.User)
                .Include(g => g.Gender)
                .Include(p=>p.Position)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            var model = new WorkerViewModel
            {
                Id = worker.Id,
                WorkerId = worker.WorkerId,
                User = worker.User,
                Gender = worker.Gender,
                GenderId = worker.Gender.Id,
                Genders = this.combosHelper.GetComboGenders(),
                PositionId= worker.Position.Id,
                Positions=this.combosHelper.GetComboPositions()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkerViewModel model)
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
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Workers") :
                        await imageHelper.UpdateImageAsync(model.ImageFile, model.User.PhotoUrl)) :
                        await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "Workers"));

                this.dataContext.Update(user);
                await dataContext.SaveChangesAsync();

                var worker = new Worker
                {
                    Id = model.Id,
                    WorkerId = model.WorkerId,
                    Gender = await this.dataContext.Genders.FindAsync(model.GenderId),
                    Position=await this.dataContext.Positions.FindAsync(model.PositionId),
                    User = await this.dataContext.Users.FindAsync(user.Id)
                };

                this.dataContext.Update(worker);

                if (worker.Position.Description != "Coordinador")
                    await userHelper.AddUserToRoleAsync(user, "Worker");
                else
                    await userHelper.AddUserToRoleAsync(user, "Coordinator");

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

            var worker = await dataContext.Workers
                .Include(u => u.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worker = await dataContext.Workers
                .Include(u => u.User)
                .FirstOrDefaultAsync(t => t.Id == id);


            dataContext.Workers.Remove(worker);

            var user = await dataContext.Users
                .FindAsync(worker.User.Id);
            dataContext.Users.Remove(user);

            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
