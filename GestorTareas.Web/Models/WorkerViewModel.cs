using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestorTareas.Web.Models
{
    public class WorkerViewModel:Worker
    {
        [DisplayName("Foto del Trabajador")]
        public IFormFile ImageFile { get; set; }
        public int PositionId { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }
    }
}
