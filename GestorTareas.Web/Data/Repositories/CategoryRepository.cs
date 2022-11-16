using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>,
        ICategoryRepository
    {
        private readonly DataContext context;

        public CategoryRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Category> GetDetailByIdAsync(int id)
        {
            return await this.context.Categories.FindAsync(id);
        }
    }
}