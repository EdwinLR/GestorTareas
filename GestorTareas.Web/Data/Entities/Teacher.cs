using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Teacher:IEntity
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public string PhotoUrl { get; set; }
        public User User { get; set; }
        public ICollection<ProjectCollaborator> ProjectCollaborators { get; set; }
        public ICollection<Activity> Activities { get; set; }

    }
}
