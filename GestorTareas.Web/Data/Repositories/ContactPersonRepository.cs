using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class ContactPersonRepository : GenericRepository<ContactPerson>, IContactPersonRepository
    {
        

    private readonly DataContext dataContext;

        public ContactPersonRepository(DataContext dataContext):base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable GetAllContactPeopleWithInstitutes()
        {
            return this.dataContext.ContactPeople
                .Include(cp => cp.Institute);
        }

        public ContactPerson GetContactPersonById(int id)
        {
            return this.dataContext.ContactPeople.Find(id);
        }

        public async Task<ContactPerson> GetContactPersonWithInstituteByIdAsync(int id)
        {
            return await this.dataContext.ContactPeople
                .Include(c => c.Institute)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
