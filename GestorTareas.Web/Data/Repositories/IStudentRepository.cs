using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IQueryable GetStudentsByCareer(int id);
    }
}