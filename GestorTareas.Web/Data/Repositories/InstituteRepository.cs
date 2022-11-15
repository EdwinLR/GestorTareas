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
                .Include(i => i.ContactPeople)
                .OrderBy(i => i.Name);
        }

        public async Task<Institute> GetInstituteWithCountryAndContactPersonAsync(int id)
        {
            return await this.context.Institutes
                .Include(i => i.Country)
                .Include(i => i.ContactPeople)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Institute> CreateInstituteAsync(Institute institute, List<InstituteDetailTemp> institutesDetails)
        {
            await this.context.Institutes.AddAsync(institute);
            this.context.InstituteDetailTemps.RemoveRange(institutesDetails);
            await this.context.SaveChangesAsync();
            return institute;
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
        public IQueryable<InstituteDetailTemp> GetAllInstituteDetailTemps()
        {
            return this.context.InstituteDetailTemps
                .Include(idt => idt.ContactPerson);
        }
        public InstituteDetailTemp GetInstituteDetailTempById(int id)
        {
            return this.context.InstituteDetailTemps.Find(id);
        }
        public InstituteDetailTemp GetInstituteDetailTempByContactId(int id)
        {
            return this.context.InstituteDetailTemps.FirstOrDefault(idt => idt.ContactPerson.Id == id);
        }
        public void AddInstituteDetailTemp(InstituteDetailTemp instituteDetail)
        {
            this.context.InstituteDetailTemps.Add(instituteDetail);
            this.context.SaveChanges();
        }
        public void DeleteInstituteDetailTemp(InstituteDetailTemp instituteDetail)
        {
            this.context.InstituteDetailTemps.Remove(instituteDetail);
            this.context.SaveChanges();
        }
    }
}