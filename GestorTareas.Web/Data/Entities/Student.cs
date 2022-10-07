using System.ComponentModel.DataAnnotations;
using System;

namespace GestorTareas.Web.Data.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La matrícula es requerido")]
        [Display(Name = "Matrícula")]
        public int StudentId { get; set; }

        public Career Career { get; set; }
        public Gender Gender { get; set; }
        public User User { get; set; }
    }
}
