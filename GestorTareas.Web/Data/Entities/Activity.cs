using System.ComponentModel.DataAnnotations;
using System;

namespace GestorTareas.Web.Data.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Título de la tarea requerido")]
        [StringLength(100)]
        [Display(Name = "Título de la tarea")]
        public string TaskTitle { get; set; }

        [Required(ErrorMessage = "Descripción requerida")]
        [StringLength(250)]
        [Display(Name = "Descripción corta")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La fecha límite requerida")]
        [Display(Name = "Fecha limíte")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DeadlineDate { get; set; }

        [Required(ErrorMessage = "La hora límite es requerida")]
        [Display(Name = "Hora límite")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:hh:mm:ss}")]
        public DateTime DeadlineTime { get; set; }
        public int Progress { get; set; }
        public DateTime CreationDate { get; set; }
        public Category Category { get; set; }
        public Priority Priority { get; set; }
        public Teacher Teacher { get; set; }

    }
}
