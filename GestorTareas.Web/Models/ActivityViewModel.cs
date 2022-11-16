using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestorTareas.Web.Models
{
    public class ActivityViewModel : Activity
    {
        public int ProjectId { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int PriorityId { get; set; }
        public IEnumerable<SelectListItem> Priorities { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}
