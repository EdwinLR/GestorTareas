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

        public Position GetPositionById(int id)
        {
            return this.context.Positions.Find(id);
        }
    }
}