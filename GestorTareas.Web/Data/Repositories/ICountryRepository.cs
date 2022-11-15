using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        //Metodos unicos de la entidad
        Task<Country> GetDetailByIdAsync(int id);
    }
}