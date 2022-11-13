using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace GestorTareas.Web.Models
{
    public class BackgroundViewModel : Background
    {

        [DisplayName("Imagen de Fondo")]
        public IFormFile ImageFile { get; set; }
    }
}
