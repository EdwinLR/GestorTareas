using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        //Metodos unicos de la entidad
        Position GetPositionById(int id);
    }
}