using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class CountryRepository : GenericRepository<Country>,
        ICountryRepository
    {
        private readonly DataContext context;

        public CountryRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Country> GetCountryByName(string name)
        {
            return await this.context.Countries.FirstOrDefaultAsync(c => c.CountryName == name);
        }

        public Country GetMasterById(int id)
        {
            return this.context.Countries.Find(id);
        }
    }
}