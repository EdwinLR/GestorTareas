using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}
