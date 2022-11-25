using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestorTareas.Web.Models
{
    public class ConvocationViewModel : Convocation
    {
        public int InstituteId { get; set; }
        public IEnumerable<SelectListItem> Institutes { get; set; }
    }
}
