using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface ICareerRepository : IGenericRepository<Career>
    {
        IQueryable<Career> GetAllCareersWithStudents();
        Career GetCareerById(int id);

        IQueryable<CareerResponse> GetAllCareersResponseWithStudents();
        CareerResponse GetCareerResponseById(int id);
    }
}
