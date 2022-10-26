using GestorTareas.Web.Data.Entities;

namespace GestorTareas.Web.Data.Repositories
{
    public class CareerRepository : GenericRepository<Career>, 
        ICareerRepository
    {
        private readonly DataContext context;

        public CareerRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
