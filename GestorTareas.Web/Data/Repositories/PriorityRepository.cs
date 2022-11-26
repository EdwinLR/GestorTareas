using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class PriorityRepository : GenericRepository<Priority>,
        IPriorityRepository
    {
        private readonly DataContext context;

        public PriorityRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Priority> GetMasterByIdAsync(int id)
        {
            return await this.context.Priorities.FindAsync(id);
        }
    }
}
