using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IStatusRepository : IGenericRepository<Status>
    {
        //Metodos unicos de la entidad
        Status GetMasterById(int id);
    }
}