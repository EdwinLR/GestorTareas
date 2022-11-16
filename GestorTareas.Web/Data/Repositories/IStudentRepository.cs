using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IQueryable GetAllStudentsWithUserAndCareerOrderByCareer();
        IQueryable GetAllStudentsWithUserAndCareerOrderByFatherLastname();
        IQueryable GetAllStudentsWithUserAndCareerOrderByCareerId(int id);
        Student GetStudentWithUserAndCareerById(int id);
        Task DeleteStudentAndUserAsync(Student student);
    }
}