using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IInstituteRepository : IGenericRepository<Institute>
    {
        //Metodos unicos de la entidad
        IQueryable<Institute> GetAllInstitutesWithCountriesAndContactPeople();
        Institute GetInstituteWithCountryAndContactPersonById(int id);
        Task<Institute> CreateInstituteAsync(Institute institute);
        Institute GetInstituteById(int id);
        IQueryable<InstituteResponse> GetAllInstitutesResponses();
        InstituteResponse GetInstituteResponseById(int id);

    }
}