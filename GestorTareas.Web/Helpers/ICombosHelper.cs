using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestorTareas.Web.Helpers
{
    public interface ICombosHelper
    {
        public IEnumerable<SelectListItem> GetComboCareers();
        public IEnumerable<SelectListItem> GetComboGenders();
        public IEnumerable<SelectListItem> GetComboCountries();
        public IEnumerable<SelectListItem> GetComboCategories();
        public IEnumerable<SelectListItem> GetComboPriorities();
        public IEnumerable<SelectListItem> GetComboStatuses();
        public IEnumerable<SelectListItem> GetComboContacts();

    }
}
