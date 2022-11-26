using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class ConvocationRepository : GenericRepository<Convocation>,
        IConvocationRepository
    {
        private readonly DataContext context;

        public ConvocationRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Convocation GetConvocationById(int id)
        {
            return this.context.Convocations.Find(id);
        }

        public IQueryable GetAllConvocationsWithInstitutes()
        {
            return this.context.Convocations
                .Include(c => c.Institute)
                .ThenInclude(c => c.Country)
                .Include(c => c.Institute)
                .ThenInclude(c => c.ContactPeople)
                .OrderBy(c => c.StartingDate);
        }

        public Convocation GetConvocationWithInstituteById(int id)
        {
            return this.context.Convocations
                .Include(c => c.Institute)
                .ThenInclude(c => c.Country)
                .Include(c => c.Institute)
                .ThenInclude(c => c.ContactPeople)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
