using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IGenderRepository : IGenericRepository<Gender>
    {
        //Metodos unicos de la entidad
        Gender GetGenderById(int id);

        IQueryable<GenderResponse> GetAllGendersResponsesWithStudents();
        GenderResponse GetGenderResponseWithStudentsById(int id);
    }
}