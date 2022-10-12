using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Country:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del país es requerido")]
        [Display(Name = "País")]
        public string CountryName { get; set; }
        public ICollection<Institute> Institutes { get; set; }
    }
}
