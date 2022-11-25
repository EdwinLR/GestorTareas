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
        Task<Institute> CreateInstituteAsync(Institute institute, List<ContactDetailTemp> institutesDetails, List<ConvocationDetailTemp> convocationDetails);
        Institute GetInstituteById(int id);

        IQueryable<ContactPerson> GetAllContactPeople();
        ContactPerson GetContactPersonById(int id);
        Task<ContactPerson> AddContactPersonAsync(ContactPerson contactPerson);
        Task<ContactPerson> UpdateContactPersonAsync(ContactPerson contactPerson);
        Task DeleteContactPersonAsync(ContactPerson contactPerson);
        Task<bool> ExistContactPersonAsync(int id);

        IQueryable<ContactDetailTemp> GetAllInstituteDetailTemps();
        ContactDetailTemp GetInstituteDetailTempById(int id);
        ContactDetailTemp GetInstituteDetailTempByContactId(int id);
        void AddInstituteDetailTemp(ContactDetailTemp instituteDetail);
        void DeleteInstituteDetailTemp(ContactDetailTemp instituteDetail);

    }
}