using System;
using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Convocation:IEntity
    {
        public int Id { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Summary { get; set; }
        public string Requirements { get; set; }
        public string Prizes { get; set; }
        public string ConvocationUrl { get; set; }
        public Institute Institute { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}