using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //Metodos unicos de la entidad
        Category GetDetailById(int id);
        Category GetCategoryByName(string name);
    }
}