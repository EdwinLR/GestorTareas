using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class ContactPerson:IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherLastName { get; set; }
        public string MotherLastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<Institute> Institutes { get; set; }
    }
}