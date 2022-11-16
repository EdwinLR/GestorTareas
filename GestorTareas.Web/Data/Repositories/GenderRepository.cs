using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestorTareas.Web.Data.Repositories
{
    public class GenderRepository : GenericRepository<Gender>,
        IGenderRepository
    {
        private readonly DataContext context;

        public GenderRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Gender GetGenderById(int id)
        {
            return this.context.Genders.Find(id);
        }
    }
}