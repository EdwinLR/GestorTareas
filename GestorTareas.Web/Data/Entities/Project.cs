using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Project:IEntity
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public Convocation Convocation { get; set; }
        public ICollection<ProjectAssigment> ProjectAssigments { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
