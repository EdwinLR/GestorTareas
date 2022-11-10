using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Background:IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Nombre de la Imagen")]
        public string Name { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        public bool EstablishedPicture { get; set; }
    }
}
