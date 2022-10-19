using GestorTareas.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GestorTareas.Web.Helpers
{
    public class CombosHelper:ICombosHelper
    {
        private readonly DataContext dataContext;

        public CombosHelper(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboPositions()
        {
            var list = this.dataContext.Positions.Select(d => new SelectListItem
            {
                Text = $"{d.Description}",
                Value = $"{d.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un puesto)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboCareers()
        {
            var list = this.dataContext.Careers.Select(f => new SelectListItem
            {
                Text = $"{f.CareerCode} {f.Name}",
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una carrera)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboCategories()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetComboContacts()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetComboCountries()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetComboGenders()
        {
            var list = this.dataContext.Genders.Select(f => new SelectListItem
            {
                Text = f.GenderName,
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un género)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboPriorities()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetComboStatuses()
        {
            throw new System.NotImplementedException();
        }

        
        
    }
}
