using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class ContactPerson: IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Apellido Paterno")]
        public string FatherLastName { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Apellido Materno")]
        public string MotherLastName { get; set; }

        [Required(ErrorMessage = "Número de teléfono requerido")]
        [Range(0, 9999999999)]
        [Display(Name = "Teléfono")]
        public long PhoneNumber { get; set; }

        [Required(ErrorMessage = "Correo electrónico requerido")]
        [MaxLength(80)]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{FatherLastName} {MotherLastName} {FirstName}";

        public Institute Institute { get; set; }
    }
}