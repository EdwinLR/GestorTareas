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
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerRepository repository;
        private readonly IPositionRepository positionRepository;
        private readonly IUserHelper userHelper;

        public WorkersController(IWorkerRepository repository, IPositionRepository positionRepository, 
            IUserHelper userHelper)
        {
            this.repository = repository;
            this.positionRepository = positionRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetWorkers()
        {
            return Ok(this.repository.GetAllWorkersResponseWithUserAndPosition());
        }

        [HttpGet("{id}")]
        public IActionResult GetWorker(int id)
        {
            var worker = this.repository.GetWorkerResponseById(id);

            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        [HttpPost]
        public async Task<IActionResult> PostWorker([FromBody] WorkerResponse workerResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByIdAsync(workerResponse.UserId);
            if (user == null)
            {
                user = new User
                {
                    FirstName = workerResponse.FirstName,
                    FatherLastName = workerResponse.FatherLastName,
                    MotherLastName = workerResponse.MotherLastName,
                    Email = workerResponse.Email,
                    PhoneNumber = workerResponse.PhoneNumber,
                    UserName = workerResponse.Email
                };
                string password = workerResponse.WorkerId + workerResponse.FatherLastName[0] + workerResponse.MotherLastName[0] + workerResponse.FirstName[0] + workerResponse.FirstName[1];
                var result = await userHelper.AddUserAsync(user, password.ToUpper());
                if (result != IdentityResult.Success)
                    return BadRequest(result);

                var position = this.positionRepository.GetPositionByName(workerResponse.Position);
                if (position == null)
                    return BadRequest("The position does not exist");

                var worker = new Worker
                {
                    Id = workerResponse.Id,
                    WorkerId = workerResponse.WorkerId,
                    Position = position,
                    User = user
                };
                var newWorker = await this.repository.CreateAsync(worker);
                if (worker.Position.Description != "Coordinador")
                    await userHelper.AddUserToRoleAsync(user, "Teacher");
                else
                    await userHelper.AddUserToRoleAsync(user, "Coordinator");
                return Ok(newWorker);
            }
            else
            {
                var worker = repository.GetWorkerWithUserAndPositionByUserId(user.Id);
                if (worker == null)
                    return BadRequest("This worker already exists");
                else
                {
                    var position = this.positionRepository.GetPositionByName(workerResponse.Position);
                    if (position == null)
                        return BadRequest("The position does not exist");

                    worker = new Worker
                    {
                        Id = workerResponse.Id,
                        WorkerId = workerResponse.WorkerId,
                        Position = position,
                        User = user
                    };
                    var newWorker = await this.repository.CreateAsync(worker);
                    await userHelper.AddUserToRoleAsync(user, "Worker");
                    return Ok(newWorker);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker([FromRoute] int id, [FromBody] WorkerResponse workerResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != workerResponse.Id)
                return BadRequest();

            var oldWorker = await this.repository.GetByIdAsync(id);

            if (oldWorker == null)
                return BadRequest("The worker doesn't exist");

            var position = this.positionRepository.GetPositionByName(workerResponse.Position);
            if (position == null)
                return BadRequest("The position does not exist");

            var user = await this.userHelper.GetUserByIdAsync(workerResponse.UserId);
            if (user == null)
                return BadRequest("The user does not exist");

            user.FirstName = workerResponse.FirstName;
            user.FatherLastName = workerResponse.FatherLastName;
            user.MotherLastName = workerResponse.MotherLastName;
            user.Email = workerResponse.Email;
            user.PhoneNumber = workerResponse.PhoneNumber;
            user.UserName = workerResponse.Email;

            oldWorker.Id = workerResponse.Id;
            oldWorker.WorkerId = workerResponse.WorkerId;
            oldWorker.Position = position;
            oldWorker.User = user;

            var result = await userHelper.UpdateUserAsync(user);
            if (result != IdentityResult.Success)
                return BadRequest();
            var updatedWorker = await repository.UpdateAsync(oldWorker);
            return Ok(updatedWorker);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var worker = await this.repository.GetByIdAsync(id);

            if (worker == null)
                return BadRequest("The worker doesn't exist");

            await repository.DeleteAsync(worker);

            return Ok(worker);
        }
    }
}
