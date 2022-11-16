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
                .Include(c => c.Convocations)
                .OrderBy(i => i.Name);
        }

        public async Task<Institute> GetInstituteWithCountryAndContactPersonAsync(int id)
        {
            return await this.context.Institutes
                .Include(i => i.Country)
                .Include(i => i.ContactPeople)
                .Include(c => c.Convocations)
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


        //Convocation Methods
        public IQueryable<Convocation> GetAllConvocations()
        {
            return this.context.Convocations
                .Include(n=>n.Institute)
                .OrderBy(i => i.Institute);
        }

        public Convocation GetConvocationById(int id)
        {
            return this.context.Convocations.Find(id);
        }

        public async Task<Convocation> AddConvocationAsync(Convocation convocation)
        {
            await this.context.Convocations.AddAsync(convocation);
            await this.context.SaveChangesAsync();
            return convocation;
        }

        public async Task<Convocation> UpdateConvocationAsync(Convocation convocation)
        {
            this.context.Convocations.Update(convocation);
            await this.context.SaveChangesAsync();
            return convocation;
        }

        public async Task DeleteConvocationAsync(Convocation convocation)
        {
            this.context.Convocations.Remove(convocation);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ExistConvocationAsync(int id)
        {
            return await this.context.Convocations.AnyAsync(cp => cp.Id == id);
        }

        public IQueryable<ConvocationDetailTemp> GetAllConvocationDetailTemps()
        {
            return this.context.ConvocationDetailTemps
                 .Include(idt => idt.Convocation);
        }

        public ConvocationDetailTemp GetConvocationDetailTempById(int id)
        {
            return this.context.ConvocationDetailTemps.Find(id);
        }

        public ConvocationDetailTemp GetInstituteDetailTempByConvocationId(int id)
        {
            return this.context.ConvocationDetailTemps.FirstOrDefault(idt => idt.Convocation.Id == id);
        }

        public void AddConvocationDetailTemp(ConvocationDetailTemp convocationDetail)
        {
            this.context.ConvocationDetailTemps.Add(convocationDetail);
            this.context.SaveChanges();
        }

        public void DeleteConvocationDetailTemp(ConvocationDetailTemp convocationDetail)
        {
            this.context.ConvocationDetailTemps.Remove(convocationDetail);
            this.context.SaveChanges();
        }
    }
}