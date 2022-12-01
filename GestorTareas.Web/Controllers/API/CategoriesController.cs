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
            var category = this.repository.GetDetailById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] CategoryResponse categoryResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != categoryResponse.Id)
                return BadRequest();

            var oldCategory = await this.repository.GetByIdAsync(id);

            if (oldCategory == null)
                return BadRequest("The category doesn't exist");

            oldCategory.CategoryName = categoryResponse.CategoryName;

            var updatedCategory = await repository.UpdateAsync(oldCategory);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await this.repository.GetByIdAsync(id);

            if (category == null)
                return BadRequest("The category doesn't exist");

            await repository.DeleteAsync(category);

            return Ok(category);
        }
    }
}
