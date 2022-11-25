using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IContactPersonRepository : IGenericRepository<ContactPerson>
    {
        IQueryable GetAllContactPeopleWithInstitutes();
        Task<ContactPerson> GetContactPersonWithInstituteByIdAsync(int id);
        ContactPerson GetContactPersonById(int id);
    }
}
