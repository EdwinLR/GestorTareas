using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        //Metodos unicos de la entidad
    }
}