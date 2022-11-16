using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GestorTareas.Web.Models
{
    public class AddStudentViewModel
    {
        [Display(Name = "Student")]
        public int StudentId { get; set; }
        public IEnumerable<SelectListItem> StudentsList { get; set; }
        public IQueryable<ActivityDetailTemp> AssignedStudents { get; set; }
    }
}
