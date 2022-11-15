using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestorTareas.Web.Models
{
    public class InstituteViewModel : Institute
    {
        public int CountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
