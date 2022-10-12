using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestorTareas.Web.Models
{
    public class StudentViewModel : Student
    {
        [DisplayName("Foto del Estudiante")]
        public IFormFile ImageFile { get; set; }
        public int CareerId { get; set; }
        public IEnumerable<SelectListItem> Careers { get; set; }
        public int GenderId { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }


    }
}
