using GestorTareas.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [MaxLength(12,ErrorMessage ="El campo {0} debe tener {1} caracteres")]
        [Display(Name ="Contraseña Actual")]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Contraseña muy corta. Debe contener mínimo 6 caracteres")]
        [MaxLength(12, ErrorMessage = "La contraseña solo puede contener 12 caracteres máximo")]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Contraseña muy corta. Debe contener mínimo 6 caracteres")]
        [MaxLength(12, ErrorMessage = "La contraseña solo puede contener 12 caracteres máximo")]
        [Display(Name = "Confirma la nueva contraseña")]
        public string RepeatedNewPassword { get; set; }
    }
}
