using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository repository;

        public ProjectsController(IProjectRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(this.repository.GetProjectsWithConvocation());
        }

        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = this.repository.GetProjectWithConvocationAndCollaboratorsByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
    }
}
