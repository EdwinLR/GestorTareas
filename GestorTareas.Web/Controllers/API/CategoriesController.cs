using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(this.repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var career = this.repository.GetDetailById(id);

            if (career == null)
            {
                return NotFound();
            }

            return Ok(career);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryResponse categoryResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Category
            {
                Id = categoryResponse.Id,
                CategoryName = categoryResponse.CategoryName

            };

            var newCategory = await this.repository.CreateAsync(category);

            return Ok(newCategory);
        }
    }
}
