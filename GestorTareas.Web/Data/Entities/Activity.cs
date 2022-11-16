using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Activity : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Título de la tarea requerido")]
        [StringLength(100)]
        [Display(Name = "Título de la tarea")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Descripción requerida")]
        [StringLength(250)]
        [Display(Name = "Descripción corta")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La fecha y hora límite requeridos")]
        [Display(Name = "Fecha y hora limíte")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Progreso")]
        public int Progress { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        [Display(Name = "Fecha de Creación")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Categoría")]
        public Category Category { get; set; }
        [Display(Name = "Prioridad")]
        public Priority Priority { get; set; }
        [Display(Name = "Estado")]
        public Status Status { get; set; }
        [Display(Name = "Proyecto")]
        public Project Project { get; set; }
        [Display(Name = "Estudiente Asignado")]
        public ICollection<Student> AssignedActivities { get; set; }

    }
}
