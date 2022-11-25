using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class InstituteRepository : GenericRepository<Institute>,
        IInstituteRepository
    {
        private readonly DataContext context;

        public InstituteRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        //Institute Methods
        IQueryable<Institute> IInstituteRepository.GetAllInstitutesWithCountriesAndContactPeople()
        {
            return this.context.Institutes
                .Include(i => i.Country)
                .OrderBy(i => i.Name);
        }

        public async Task<Institute> GetInstituteWithCountryAndContactPersonAsync(int id)
        {
            return await this.context.Institutes
                .Include(i => i.Country)
                .Include(i=>i.Convocations)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Institute> CreateInstituteAsync(Institute institute, List<ContactDetailTemp> institutesDetails, List<ConvocationDetailTemp> convocationDetails)
        {
            await this.context.Institutes.AddAsync(institute);
            this.context.ContactDetailTemps.RemoveRange(institutesDetails);
            this.context.ConvocationDetailTemps.RemoveRange(convocationDetails);
            await this.context.SaveChangesAsync();
            return institute;
        }

        public Institute GetInstituteById(int id)
        {
            return this.context.Institutes.Find(id);
        }


        //ContactPerson Methods
        public IQueryable<ContactPerson> GetAllContactPeople()
        {
            return this.context.ContactPeople
                .Include(cp => cp.Institute);
        }

        public ContactPerson GetContactPersonById(int id)
        {
            return this.context.ContactPeople.Find(id);
        }

        public async Task<ContactPerson> AddContactPersonAsync(ContactPerson contactPerson)
        {
            await this.context.ContactPeople.AddAsync(contactPerson);
            await this.context.SaveChangesAsync();
            return contactPerson;
        }

        public async Task<ContactPerson> UpdateContactPersonAsync(ContactPerson contactPerson)
        {
            this.context.ContactPeople.Update(contactPerson);
            await this.context.SaveChangesAsync();
            return contactPerson;
        }

        public async Task DeleteContactPersonAsync(ContactPerson contactPerson)
        {
            this.context.ContactPeople.Remove(contactPerson);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ExistContactPersonAsync(int id)
        {
            return await this.context.ContactPeople.AnyAsync(cp => cp.Id == id);
        }


        //InstituteDetailTemp Methods
        public IQueryable<ContactDetailTemp> GetAllInstituteDetailTemps()
        {
            return this.context.ContactDetailTemps
                .Include(idt => idt.ContactPerson);
        }
        public ContactDetailTemp GetInstituteDetailTempById(int id)
        {
            return this.context.ContactDetailTemps.Find(id);
        }
        public ContactDetailTemp GetInstituteDetailTempByContactId(int id)
        {
            return this.context.ContactDetailTemps.FirstOrDefault(idt => idt.ContactPerson.Id == id);
        }
        public void AddInstituteDetailTemp(ContactDetailTemp instituteDetail)
        {
            this.context.ContactDetailTemps.Add(instituteDetail);
            this.context.SaveChanges();
        }
        public void DeleteInstituteDetailTemp(ContactDetailTemp instituteDetail)
        {
            this.context.ContactDetailTemps.Remove(instituteDetail);
            this.context.SaveChanges();
        }

    }
}