using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestorTareas.Web.Data.Repositories
{
    public class ContactPersonRepository : GenericRepository<ContactPerson>,
        IContactPersonRepository
    {
        private readonly DataContext context;

        public ContactPersonRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}