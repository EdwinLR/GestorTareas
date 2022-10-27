using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestorTareas.Web.Data.Repositories
{
    public class CareerRepository : GenericRepository<Career>,
        ICareerRepository
    {
        private readonly DataContext context;

        public CareerRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
        public IQueryable GetCareersWithStudents()
        {
            return this.context.Careers
                .Include(c => c.Students)
                .OrderBy(c => c.Name);
        }
    }
}
