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

        public Priority GetMasterById(int id)
        {
            return this.context.Priorities.Find(id);
        }
    }
}
