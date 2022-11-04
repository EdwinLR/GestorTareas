using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Convocation : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fecha de Inicio requerida")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartingDate { get; set; }

        [Required(ErrorMessage = "Fecha de Termino requerida")]
        [Display(Name = "Fecha de Termino")]
        public DateTime EndingDate { get; set; }

        [Required(ErrorMessage = "Resumen requerido")]
        [StringLength(150)]
        [Display(Name = "Resumen")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Requisitos requeridos")]
        [StringLength(300)]
        [Display(Name = "Requisitos")]
        public string Requirements { get; set; }

        [Required(ErrorMessage = "Premios requeridos")]
        [StringLength(200)]
        [Display(Name = "Premios")]
        public string Prizes { get; set; }

        [Required(ErrorMessage = "URL de Convocatoria requerido")]
        [StringLength(100)]
        [Display(Name = "URL de la Convocatoria")]
        public string ConvocationUrl { get; set; }
        public Institute Institute { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}