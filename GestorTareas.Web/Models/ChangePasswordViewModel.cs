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
        [MaxLength(12, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        [Display(Name = "Confirma la nueva contraseña")]
        public string RepeatedNewPassword { get; set; }
    }
}
