using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Institute:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public ICollection<Convocation> Annoucements { get; set; }

        //Pendiente: Agregar dirección (Ciudad,Calle, número, colonia)
    }
}