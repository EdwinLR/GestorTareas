using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
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

            var position = this.positionRepository.GetPositionByName(workerResponse.Position);
            if (position == null)
                return BadRequest("The position does not exist");

            var user = await this.userHelper.GetUserByIdAsync(workerResponse.UserId);
            if (user == null)
            {
                //Create user
                //Create worker
            }
                

            var worker = new Worker
            {
                Id = workerResponse.Id,
                WorkerId = workerResponse.WorkerId,
                Position = position,
                User = user
            };

            var newWorker = await this.repository.CreateAsync(worker);

            return Ok(newWorker);
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

            //Change user properties
            //user.FirstName = workerResponse.FirstName;

            oldWorker.Id = workerResponse.Id;
            oldWorker.WorkerId = workerResponse.WorkerId;
            oldWorker.Position = position;
            oldWorker.User = user;

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
