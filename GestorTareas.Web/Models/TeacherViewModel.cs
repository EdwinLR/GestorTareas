using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestorTareas.Web.Models
{
    public class TeacherViewModel:Teacher
    {
        [DisplayName("Foto del Docente")]
        public IFormFile ImageFile { get; set; }
        public int GenderId { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
    }
}
