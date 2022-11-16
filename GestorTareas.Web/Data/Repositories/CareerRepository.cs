using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public IQueryable<Career> GetAllICareerWithStudents()
        {
            return this.context.Careers
                .Include(c => c.Students);
        }

        public Career GetCareerById(int id)
        {
            return this.context.Careers.Find(id);
        }
    }
}
