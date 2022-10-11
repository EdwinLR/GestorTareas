using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Status : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre del estado requerido")]
        [StringLength(30)]
        [Display(Name = "Estado")]
        public string Name { get; set; }

        public ICollection<Activity> AssignedTasks { get; set; }
    }
}
