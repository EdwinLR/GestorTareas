using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class InstituteRepository : GenericRepository<Institute>,
        IInstituteRepository
    {
        private readonly DataContext context;

        public InstituteRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        //Institute Methods
        IQueryable<Institute> IInstituteRepository.GetAllInstitutesWithCountriesAndContactPeople()
        {
            return this.context.Institutes
                .Include(i => i.Country)
                .Include(i => i.ContactPeople)
                .Include(i => i.Convocations)
                .OrderBy(i => i.Name);
        }

        public Institute GetInstituteWithCountryAndContactPersonById(int id)
        {
            return this.context.Institutes
                .Include(i => i.Country)
                .Include(i => i.Convocations)
                .Include(i => i.ContactPeople)
                .FirstOrDefault(i => i.Id == id);
        }

        public async Task<Institute> CreateInstituteAsync(Institute institute)
        {
            await this.context.Institutes.AddAsync(institute);
            await this.context.SaveChangesAsync();
            return institute;
        }

        public Institute GetInstituteById(int id)
        {
            return this.context.Institutes.Find(id);
        }

        public IQueryable<InstituteResponse> GetAllInstitutesResponses()
        {
            return this.context.Institutes
               .Select(i => new InstituteResponse
               {
                   Id = i.Id,
                   Name = i.Name,
                   City = i.City,
                   District = i.District,
                   ContactPhone = i.ContactPhone,
                   Country = i.Country.CountryName,
                   StreetName = i.StreetName,
                   StreetNumber = i.StreetNumber,
                   ContactPeople = i.ContactPeople.Select(cp => new ContactPersonResponse
                   {
                       Id = cp.Id,
                       FirstName = cp.FirstName,
                       FatherLastName = cp.FatherLastName,
                       MotherLastName = cp.MotherLastName,
                       Email = cp.Email,
                       Institute = cp.Institute.Name,
                       PhoneNumber = cp.PhoneNumber

                   }),
                   Convocations = i.Convocations.Select(c => new ConvocationResponse
                   {
                       Id = c.Id,
                       Summary = c.Summary,
                       StartingDate = c.StartingDate,
                       EndingDate = c.EndingDate,
                       ConvocationUrl = c.ConvocationUrl,
                       Prizes = c.Prizes,
                       Institute = c.Institute.Name,
                       Requirements = c.Requirements
                   })
               });
        }

        public InstituteResponse GetInstituteResponseById(int id)
        {
            return this.context.Institutes
              .Select(i => new InstituteResponse
              {
                  Id = i.Id,
                  Name = i.Name,
                  City = i.City,
                  District = i.District,
                  Country = i.Country.CountryName,
                  StreetName = i.StreetName,
                  StreetNumber = i.StreetNumber,
                  ContactPeople = i.ContactPeople.Select(cp => new ContactPersonResponse
                  {
                      Id = cp.Id,
                      FirstName = cp.FirstName,
                      FatherLastName = cp.FatherLastName,
                      MotherLastName = cp.MotherLastName,
                      Email = cp.Email,
                      Institute = cp.Institute.Name,
                      PhoneNumber = cp.PhoneNumber

                  }),
                  Convocations = i.Convocations.Select(c => new ConvocationResponse
                  {
                      Id = c.Id,
                      Summary = c.Summary,
                      StartingDate = c.StartingDate,
                      EndingDate = c.EndingDate,
                      ConvocationUrl = c.ConvocationUrl,
                      Prizes = c.Prizes,
                      Institute = c.Institute.Name,
                      Requirements = c.Requirements
                  })
              }).FirstOrDefault(i => i.Id == id);
        }
    }
}