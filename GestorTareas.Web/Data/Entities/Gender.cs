using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Gender : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre del genéro requerido")]
        [StringLength(15)]
        [Display(Name = "Género")]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
