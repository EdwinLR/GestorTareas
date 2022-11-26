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
                .Include(i => i.Convocations)
                .OrderBy(i => i.Name);
        }

        public Institute GetInstituteWithCountryAndContactPersonById(int id)
        {
            return this.context.Institutes
                .Include(i => i.Country)
                .Include(i=>i.Convocations)
                .Include(i=>i.ContactPeople)
                .FirstOrDefault(i => i.Id == id);
        }

        public async Task<Institute> CreateInstituteAsync(Institute institute)
        {
            await this.context.Institutes.AddAsync(institute);
            await this.context.SaveChangesAsync();
            return institute;
        }

        public Institute GetInstituteById(int id)
        {
            return this.context.Institutes.Find(id);
        }

    }
}