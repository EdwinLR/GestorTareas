using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Position : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del puesto es requerido")]
        [Display(Name = "Puesto")]
        public string Description { get; set; }

        public ICollection<Worker> Workers { get; set; }
    }
}
