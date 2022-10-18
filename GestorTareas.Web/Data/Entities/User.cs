using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class User:IdentityUser
    {
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

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{FatherLastName} {MotherLastName} {FirstName}";

        [Display(Name = "Foto")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Celular")]
        public override string PhoneNumber { get; set; }
    }
}
