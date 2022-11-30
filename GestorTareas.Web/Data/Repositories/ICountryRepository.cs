using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        //Metodos únicos de la entidad
        Country GetMasterById(int id);

        Task<Country> GetCountryByName(string name);
    }
}