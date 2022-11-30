using GestorTareas.Common.Models;
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

        public IQueryable<ConvocationResponse> GetAllConvocationsResponses()
        {
            return this.context.Convocations
                .Select(c => new ConvocationResponse
                {
                    Id = c.Id,
                    Summary = c.Summary,
                    StartingDate = c.StartingDate,
                    EndingDate = c.EndingDate,
                    Prizes = c.Prizes,
                    ConvocationUrl = c.ConvocationUrl,
                    Requirements = c.Requirements,
                    Institute = c.Institute.Name
                });
        }

        public ConvocationResponse GetConvocationResponseById(int id)
        {
            return this.context.Convocations
                .Select(c => new ConvocationResponse
                {
                    Id = c.Id,
                    Summary = c.Summary,
                    StartingDate = c.StartingDate,
                    EndingDate = c.EndingDate,
                    Prizes = c.Prizes,
                    ConvocationUrl = c.ConvocationUrl,
                    Requirements = c.Requirements,
                    Institute = c.Institute.Name
                }).FirstOrDefault(c=>c.Id==id);
        }
    }
}
