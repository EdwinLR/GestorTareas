using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class ActivityDetailTemp : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Actividad")]
        public Student Student { get; set; }
    }
}
