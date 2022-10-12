using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Coordinator:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de trabajador es requerido")]
        [Display(Name = "Número de Trabajador")]
        public int WorkerId { get; set; }
        public User User { get; set; }
        public Gender Gender { get; set; }
    }
}
