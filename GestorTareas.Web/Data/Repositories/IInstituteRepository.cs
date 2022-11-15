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
        Task<Institute> GetInstituteWithCountryAndContactPersonAsync(int id);
        Task<Institute> CreateInstituteAsync(Institute institute, List<InstituteDetailTemp> institutesDetails);

        IQueryable<ContactPerson> GetAllContactPeople();
        ContactPerson GetContactPersonById(int id);
        Task<ContactPerson> AddContactPersonAsync(ContactPerson contactPerson);
        Task<ContactPerson> UpdateContactPersonAsync(ContactPerson contactPerson);
        Task DeleteContactPersonAsync(ContactPerson contactPerson);
        Task<bool> ExistContactPersonAsync(int id);

        IQueryable<InstituteDetailTemp> GetAllInstituteDetailTemps();
        InstituteDetailTemp GetInstituteDetailTempById(int id);
        InstituteDetailTemp GetInstituteDetailTempByContactId(int id);
        void AddInstituteDetailTemp(InstituteDetailTemp instituteDetail);
        void DeleteInstituteDetailTemp(InstituteDetailTemp instituteDetail);
    }
}