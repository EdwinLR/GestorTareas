using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Models
{
    public class ContactPersonViewModel:ContactPerson
    {
        public int InstituteId { get; set; }
        public IEnumerable<SelectListItem> Institutes { get; set; }
    }
}
