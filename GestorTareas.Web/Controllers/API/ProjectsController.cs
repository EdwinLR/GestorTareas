using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository repository;
        private readonly IConvocationRepository convocationRepository;

        public ProjectsController(IProjectRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(this.repository.GetAllProjectsResponse());
        }

        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = this.repository.GetProjectResponseById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] ProjectResponse projectResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var convocation = convocationRepository.GetConvocationByName(projectResponse.Convocation);
            if (convocation == null)
                return BadRequest("The convocation does not exist");

            var project = new Project
            {
                Id = projectResponse.Id,
                ProjectName = projectResponse.ProjectName,
                Convocation = convocation
            };

            var newProject = await this.repository.CreateAsync(project);

            return Ok(newProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] ProjectResponse projectResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != projectResponse.Id)
                return BadRequest();

            var oldProject = await this.repository.GetByIdAsync(id);
            if (oldProject == null)
                return BadRequest("The project does not exist.");

            var convocation = convocationRepository.GetConvocationByName(projectResponse.Convocation);
            if (convocation == null)
                return BadRequest("The convocation does not exist");

            oldProject.ProjectName = projectResponse.ProjectName;
            oldProject.Convocation = convocation;

            var updatedProject = await this.repository.UpdateAsync(oldProject);

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await this.repository.GetByIdAsync(id);
            if (project == null)
                return BadRequest("Project not exist");

            await repository.DeleteAsync(project);
            return Ok(project);
        }
    }
}
