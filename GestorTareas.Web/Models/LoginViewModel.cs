using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recuérdame")]
        public bool RememberMe { get; set; }
    }
}
