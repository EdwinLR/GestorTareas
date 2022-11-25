using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Institute : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de la institución requerido")]
        [StringLength(60)]
        [Display(Name = "Institución")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Teléfono de contacto requerido")]
        [StringLength(12)]
        [Display(Name = "Teléfono de contacto")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Calle de la dirección requerida")]
        [StringLength(30)]
        [Display(Name = "Calle")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Número de la dirección requerido")]
        [StringLength(5)]
        [Display(Name = "Número")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "Nombre del distrito/colonia requerido")]
        [StringLength(30)]
        [Display(Name = "Distrito o Colonia")]
        public string District { get; set; }

        [Required(ErrorMessage = "Nombre de la ciudad requerido")]
        [StringLength(30)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Display(Name = "País")]
        public Country Country { get; set; }

        [Display(Name = "Persona de contacto")]
        public ICollection<ContactPerson> ContactPeople { get; set; }
        public ICollection<Convocation> Convocations { get; set; }
    }
}