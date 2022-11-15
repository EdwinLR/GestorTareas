using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Worker : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de trabajador es requerido")]
        [StringLength(4, ErrorMessage = "El número de trabajador solo puede tener 4 caracteres")]
        [Display(Name = "Número de Trabajador")]
        public string WorkerId { get; set; }
        public Position Position { get; set; }
        public User User { get; set; }
        public ICollection<ProjectAssigment> ProjectAssigments { get; set; }
    }
}
