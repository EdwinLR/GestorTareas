namespace GestorTareas.Web.Data.Entities
{
    public class ProjectCollaborator:IEntity
    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Project Project { get; set; }

    }
}
