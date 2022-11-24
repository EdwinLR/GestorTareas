using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GestorTareas.Web.Models
{
    public class AddContactPersonViewModel
    {
        [Display(Name = "Persona de Contacto")]
        public int ContactPersonId { get; set; }
        public int InstituteId { get; set; }
        public IEnumerable<SelectListItem> ContactPeopleList { get; set; }
        public IQueryable<ContactDetailTemp> AssignedContactPeople { get; set; }
    }
}
