namespace GestorTareas.Web.Data.Entities
{
    public class ConvocationDetailTemp:IEntity
    {
        public int Id { get; set; }
        public Convocation Convocation { get; set; }
    }
}
