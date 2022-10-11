using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de la categoría requerido")]
        [StringLength(30)]
        [Display(Name = "Categoría")]
        public string Name { get; set; }

        public ICollection<Activity> AssignedTasks { get; set; }
    }
}
