using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IConvocationRepository : IGenericRepository<Convocation>
    {
        //Metodos unicos de la entidad
        IQueryable GetAllConvocationsWithInstitutes();
        Convocation GetConvocationWithInstituteById(int id);

        Convocation GetConvocationById(int id);
    }
}
