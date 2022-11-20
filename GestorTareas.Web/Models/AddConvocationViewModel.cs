using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GestorTareas.Web.Models
{
    public class AddConvocationViewModel
    {
        [Display(Name = "Convocatoria")]
        public int ConvocationId { get; set; }
        public IEnumerable<SelectListItem> ConvocationList { get; set; }
        public IQueryable<ConvocationDetailTemp> ConvocationDetails { get; set; }
    }
}
