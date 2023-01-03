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
    public class WorkersController : Controller
    {
        private readonly DataContext context;
        private readonly IWorkerRepository repository;
        private readonly IPositionRepository positionRepository;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;

        public WorkersController(DataContext context, IWorkerRepository repository,
           IPositionRepository positionRepository, IImageHelper imageHelper,
           IUserHelper userHelper, ICombosHelper combosHelper)
        {
            this.context = context;
            this.repository = repository;
            this.positionRepository = positionRepository;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Index(string sortOrder)
        {
            var studentsList = repository.GetAllWorkersWithUserAndPositionOrderByFatherLastname();

            if (sortOrder == "name_asc")
            {
                studentsList = repository.GetAllWorkersWithUserAndPositionOrderByFatherLastname();
                return View(studentsList);
            }

            if (sortOrder == "position_asc")
            {
                studentsList = repository.GetAllWorkersWithUserAndPositionOrderByPosition();
                return View(studentsList);
            }
            return View(studentsList);

        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = repository.GetWorkerWithUserAndPositionById(id.Value);

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
                    string password = model.WorkerId.ToString() + model.User.FatherLastName[0] + model.User.MotherLastName[0] + model.User.FirstName[0] + model.User.FirstName[1];
                    var result = await userHelper.AddUserAsync(user, password.ToUpper());
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("ERROR. No se pudo crear el usuario.");
                    }

                }

                var worker = new Worker
                {
                    WorkerId = model.WorkerId,
                    Position = this.positionRepository.GetPositionWithWorkersById(model.PositionId),
                    User = await this.context.Users.FindAsync(user.Id)
                };

                if (worker.Position.Description != "Coordinador")
                    await userHelper.AddUserToRoleAsync(user, "Teacher");
                else
                    await userHelper.AddUserToRoleAsync(user, "Coordinator");

                await repository.CreateAsync(worker);
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

            var worker = repository.GetWorkerWithUserAndPositionById(id.Value);

            if (worker == null)
            {
                return NotFound();
            }

            var model = new WorkerViewModel
            {
                Id = worker.Id,
                WorkerId = worker.WorkerId,
                User = worker.User,
                PositionId = worker.Position.Id,
                Positions = this.combosHelper.GetComboPositions()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.context.Users.FindAsync(model.User.Id);
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

                this.context.Users.Update(user);
                await this.context.SaveChangesAsync();

                var worker = new Worker
                {
                    Id = model.Id,
                    WorkerId = model.WorkerId,
                    Position = await this.positionRepository.GetByIdAsync(model.PositionId),
                    User = await this.context.Users.FindAsync(user.Id)
                };

                if (worker.Position.Description != "Coordinador")
                    await userHelper.AddUserToRoleAsync(user, "Teacher");
                else
                    await userHelper.AddUserToRoleAsync(user, "Coordinator");

                await repository.UpdateAsync(worker);
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

            var worker = repository.GetWorkerWithUserAndPositionById(id.Value);

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
            var worker = repository.GetWorkerWithUserAndPositionById(id);
            await repository.DeleteAsync(worker);

            var user = await context.Users.FindAsync(worker.User.Id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
