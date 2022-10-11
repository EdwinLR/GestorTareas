using System.Collections.Generic;

namespace GestorTareas.Web.Data.Entities
{
    public class Project:IEntity
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public Annoucement Annoucement { get; set; }
        public ICollection<ProjectCollaborator> ProjectCollaborators { get; set; }
        public ICollection<AssignedActivity> AssignedActivities{ get; set; }
    }
}
