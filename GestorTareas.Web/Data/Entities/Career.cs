using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Career : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Código de la carrera requerido")]
        [StringLength(3)]
        [Display(Name = "Código de la Carrera")]
        public string CareerCode { get; set; }

        [Required(ErrorMessage = "Nombre de la carrera requerido")]
        [StringLength(60)]
        [Display(Name = "Carrera")]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
