using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}