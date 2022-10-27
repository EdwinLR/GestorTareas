using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable GetAllInstitutesWithCountriesAndContactPeople()
        {
            return this.context.Institutes
                .Include(i => i.Country)
                .Include(i => i.ContactPerson)
                .OrderBy(i => i.Name);
        }

        public async Task<Institute> GetInstituteWithCountryAndContactPersonAsync(int id)
        {
            return await this.context.Institutes
                .Include(i => i.Country)
                .Include(i => i.ContactPerson)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}