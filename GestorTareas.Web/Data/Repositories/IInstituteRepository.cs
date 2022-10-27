using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IInstituteRepository : IGenericRepository<Institute>
    {
        //Metodos unicos de la entidad
        IQueryable GetAllInstitutesWithCountriesAndContactPeople();
        Task<Institute> GetInstituteWithCountryAndContactPersonAsync(int id);
    }
}