using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GestorTareas.Web.Models
{
    public class AddCollaboratorViewModel:IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IQueryable<ProjectCollaboratorsDetailTemp> AssignedUsers { get; set; }
        public int ProjectId { get; set; }


    }
}
