using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class WorkerRepository : GenericRepository<Worker>,
        IWorkerRepository
    {
        private readonly DataContext context;

        public WorkerRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWorkersWithUserAndPositionOrderByPosition()
        {
            return this.context.Workers
                        .Include(u => u.User)
                        .Include(p => p.Position)
                        .OrderBy(p => p.Position.Description);
        }

        public IQueryable GetAllWorkersWithUserAndPositionOrderByFatherLastname()
        {
            return this.context.Workers
                        .Include(u => u.User)
                        .Include(p => p.Position)
                        .OrderBy(u => u.User.FatherLastName);
        }

        public Worker GetWorkerWithUserAndPositionById(int id)
        {
            return context.Workers
                .Include(u => u.User)
                .Include(p => p.Position)
                .FirstOrDefault(w => w.Id == id);
        }
    }
}

