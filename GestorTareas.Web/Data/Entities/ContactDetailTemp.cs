using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class ContactDetailTemp : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Persona de Contacto")]
        public ContactPerson ContactPerson { get; set; }
    }
}
