using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface ICareerRepository : IGenericRepository<Career>
    {
        IQueryable<Career> GetAllICareerWithStudents();
        Career GetCareerById(int id);
    }
}
