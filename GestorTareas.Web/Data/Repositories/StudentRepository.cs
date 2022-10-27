using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}
