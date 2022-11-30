using GestorTareas.Common.Models;
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
                .Include(cp => cp.Institute)
                .ThenInclude(c => c.Country);
        }

        public ContactPerson GetContactPersonById(int id)
        {
            return this.dataContext.ContactPeople
                .Include(cp => cp.Institute)
                .ThenInclude(c => c.Country)
                .FirstOrDefault(cp => cp.Id == id);
        }

        public async Task<ContactPerson> GetContactPersonWithInstituteByIdAsync(int id)
        {
            return await this.dataContext.ContactPeople
                .Include(c => c.Institute)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<ContactPersonResponse> GetAllContactPeopleResponses()
        {
            return this.dataContext.ContactPeople
              .Select(i => new ContactPersonResponse
              {
                  Id = i.Id,
                  Email = i.Email,
                  FirstName = i.FirstName,
                  FatherLastName = i.FatherLastName,
                  MotherLastName = i.MotherLastName,
                  PhoneNumber = i.PhoneNumber,
                  Institute = i.Institute.Name
              });
        }

        public ContactPersonResponse GetContactPersonResponseById(int id)
        {
            return this.dataContext.ContactPeople
              .Select(i => new ContactPersonResponse
              {
                  Id = i.Id,
                  Email = i.Email,
                  FirstName = i.FirstName,
                  FatherLastName = i.FatherLastName,
                  MotherLastName = i.MotherLastName,
                  PhoneNumber = i.PhoneNumber,
                  Institute = i.Institute.Name
              }).FirstOrDefault(i=>i.Id==id);
        }
    }
}
