using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestorTareas.Web.Helpers
{
    public interface ICombosHelper
    {
        public IEnumerable<SelectListItem> GetComboCareers();
        public IEnumerable<SelectListItem> GetComboCategories();
        public IEnumerable<SelectListItem> GetComboContacts();
        public IEnumerable<SelectListItem> GetComboConvocations();
        public IEnumerable<SelectListItem> GetComboCountries();
        public IEnumerable<SelectListItem> GetComboGenders();
        public IEnumerable<SelectListItem> GetComboInstitutes();
        public IEnumerable<SelectListItem> GetComboPositions();
        public IEnumerable<SelectListItem> GetComboPriorities();
        public IEnumerable<SelectListItem> GetComboStatuses();
        public IEnumerable<SelectListItem> GetComboUsers();
        public IEnumerable<SelectListItem> GetComboProjects();

    }
}
