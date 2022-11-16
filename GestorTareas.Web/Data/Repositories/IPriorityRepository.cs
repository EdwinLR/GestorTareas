using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IPriorityRepository : IGenericRepository<Priority>
    {
        //Metodos unicos de la entidad
        Task<Priority> GetDetailByIdAsync(int id);
    }
}