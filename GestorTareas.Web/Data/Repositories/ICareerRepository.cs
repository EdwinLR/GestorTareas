using GestorTareas.Web.Data.Entities;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface ICareerRepository : IGenericRepository<Career>
    {
        //Task<Career> GetCareerByIdWithStudents(int id);
        //Task<Career> GetCareerByIdWithProjects(int id);
    }
}
