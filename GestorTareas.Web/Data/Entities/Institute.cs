using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Institute:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public ICollection<Convocation> Annoucements { get; set; }
    }
}