using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Career : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de la carrera requerido")]
        [StringLength(60)]
        [Display(Name = "Carrera")]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
