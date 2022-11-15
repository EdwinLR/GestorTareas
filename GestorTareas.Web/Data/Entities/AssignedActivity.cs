namespace GestorTareas.Web.Data.Entities
{
    public class AssignedActivity:IEntity
    {
        public int Id { get; set; }
        public Activity Activity { get; set; }
        public Student Student { get; set; }
    }
}
