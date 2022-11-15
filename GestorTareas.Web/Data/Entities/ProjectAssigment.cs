namespace GestorTareas.Web.Data.Entities
{
    public class ProjectAssigment:IEntity
    {
        public int Id { get; set; }
        public Worker Worker { get; set; }
        public Project Project { get; set; }

    }
}
