using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestorTareas.Web.Data.Repositories
{
    public class PositionRepository : GenericRepository<Position>,
        IPositionRepository
    {
        private readonly DataContext context;

        public PositionRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<Position> GetAllPositionsWithWorkers()
        {
            return this.context.Positions
                .Include(c => c.Workers)
                .ThenInclude(u => u.User);
        }

        public Position GetPositionWithWorkersById(int id)
        {
            return this.context.Positions
                .Include(c => c.Workers)
                .ThenInclude(u => u.User)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}