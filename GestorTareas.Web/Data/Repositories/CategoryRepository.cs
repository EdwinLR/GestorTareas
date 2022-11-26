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

        public Category GetDetailById(int id)
        {
            return this.context.Categories.Find(id);
        }
    }
}