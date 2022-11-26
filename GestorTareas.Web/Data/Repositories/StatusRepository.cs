using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class StatusRepository : GenericRepository<Status>,
        IStatusRepository
    {
        private readonly DataContext context;

        public StatusRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Status GetMasterById(int id)
        {
            return this.context.Statuses.Find(id);
        }
    }
}
