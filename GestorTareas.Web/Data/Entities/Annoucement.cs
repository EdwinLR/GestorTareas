using System;
using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Annoucement:IEntity
    {
        public int Id { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Description { get; set; }
        public Institute Institute { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}