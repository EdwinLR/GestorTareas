using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestorTareas.Web.Models
{
    public class ProjectViewModel:Project
    {
        public int ConvocationId { get; set; }
        public IEnumerable<SelectListItem> Convocations { get; set; }
    }
}
