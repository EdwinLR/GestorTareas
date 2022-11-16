using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class ProjectCollaboratorsDetailTemp:IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Colaborador")]
        public User User { get; set; }

        public Project Project { get; set; }
    }
}
