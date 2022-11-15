using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La matrícula es requerida")]
        [StringLength(8, ErrorMessage = "La matrícula solo puede tener 8 caracteres")]
        [Display(Name = "Matrícula")]
        public string StudentId { get; set; }
        public Career Career { get; set; }
        public Gender Gender { get; set; }
        public ICollection<AssignedActivity> AssignedActivities { get; set; }
        public User User { get; set; }
    }
}
