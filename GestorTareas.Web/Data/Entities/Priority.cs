using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Priority : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de la prioridad requerido")]
        [StringLength(30)]
        [Display(Name = "Prioridad")]
        public string PriorityName { get; set; }
    }
}
