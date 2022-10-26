using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class InstitutesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;

        public InstitutesController(DataContext context,
            ICombosHelper combosHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Institutes
                .Include(c => c.Country)
                .Include(c => c.ContactPerson)
                .ToListAsync());
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .Include(c => c.Country)
                .Include(c => c.ContactPerson)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public IActionResult Create()
        {
            var model = new InstituteViewModel
            {
                Countries = this.combosHelper.GetComboCountries(),
                ContactPeople = this.combosHelper.GetComboContacts()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstituteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var institute = new Institute
                {
                    Name = model.Name,
                    ContactPhone = model.ContactPhone,
                    StreetName = model.StreetName,
                    StreetNumber = model.StreetNumber,
                    District = model.District,
                    City = model.City,
                    Country = await this._context.Countries.FindAsync(model.CountryId),
                    ContactPerson = await this._context.ContactPeople.FindAsync(model.ContactPersonId)
                };

                _context.Add(institute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .Include(c => c.Country)
                .Include(c => c.ContactPerson)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (institute == null)
            {
                return NotFound();
            }
            var model = new InstituteViewModel
            {
                Id = institute.Id,
                Name = institute.Name,
                ContactPhone = institute.ContactPhone,
                StreetName = institute.StreetName,
                StreetNumber = institute.StreetNumber,
                District = institute.District,
                City = institute.City,
                Country = institute.Country,
                CountryId = institute.Country.Id,
                Countries = this.combosHelper.GetComboCountries(),
                ContactPerson = institute.ContactPerson,
                ContactPersonId = institute.ContactPerson.Id,
                ContactPeople = this.combosHelper.GetComboContacts()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InstituteViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Coordinator,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .Include(c => c.Country)
                .Include(c => c.ContactPerson)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institute = await _context.Institutes
                .Include(c => c.Country)
                .Include(c => c.ContactPerson)
                .FirstOrDefaultAsync(i => i.Id == id);
            _context.Institutes.Remove(institute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteExists(int id)
        {
            return _context.Institutes.Any(i => i.Id == id);
        }
    }
}
