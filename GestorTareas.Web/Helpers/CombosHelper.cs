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
            var list = this.dataContext.Categories.Select(f => new SelectListItem
            {
                Text = $"{f.CategoryName}",
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una categoría)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboContacts()
        {
            var list = this.dataContext.ContactPeople.Select(f => new SelectListItem
            {
                Text = $"{f.FullName}",
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una persona de contacto)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboCountries()
        {
            var list = this.dataContext.Countries.Select(f => new SelectListItem
            {
                Text = $"{f.CountryName}",
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un país)",
                Value = "0"
            });
            return list;
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
            var list = this.dataContext.Priorities.Select(f => new SelectListItem
            {
                Text = $"{f.PriorityName}",
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona una prioridad)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboStatuses()
        {
            var list = this.dataContext.Statuses.Select(f => new SelectListItem
            {
                Text = $"{f.StatusName}",
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un estado)",
                Value = "0"
            });
            return list;
        }


        public IEnumerable<SelectListItem> GetComboInstitutes()
        {
            var list = this.dataContext.Institutes.Select(f => new SelectListItem
            {
                Text = $"{f.Name}",
                Value = $"{f.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un instituto)",
                Value = "0"
            });
            return list;
        }
    }
}
