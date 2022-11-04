using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface ICareerRepository : IGenericRepository<Career>
    {
        //Task<Career> GetCareerByIdWithProjects(int id);

    }
}
