using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Web.Data.Entities
{
    public class Project:IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nombre del Proyecto")]
        public string ProjectName { get; set; }
        public Convocation Convocation { get; set; }
        public ICollection<ProjectCollaborator> ProjectCollaborators { get; set; }
    }
}
