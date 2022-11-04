using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>,
        IStudentRepository
    {
        private readonly DataContext context;

        public StudentRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetStudentsByCareer(int id)
        {
            return this.context.Students
                .Include(u => u.User)
                .Where(c => c.Career.Id == id);


        }
    }
}
