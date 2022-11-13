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
    public class BackgroundsController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IImageHelper imageHelper;

        public BackgroundsController(DataContext dataContext, IImageHelper imageHelper)
        {
            this.dataContext = dataContext;
            this.imageHelper = imageHelper;
        }

        public async Task<IActionResult> Index()
        {
            var images = await this.dataContext.Backgrounds.ToListAsync();

            return View(images);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNewBackgroundPicture()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBackgroundPicture(BackgroundViewModel backgroundViewModel)
        {
            if (ModelState.IsValid)
            {
                var background = new Background
                {
                    Name = backgroundViewModel.Name,
                    PhotoUrl = await imageHelper.AddImageAsync(backgroundViewModel.ImageFile, backgroundViewModel.Name, "BackgroundPictures"),
                    EstablishedPicture = false
                };

                this.dataContext.Backgrounds.Add(background);
                await this.dataContext.SaveChangesAsync();
                return RedirectToAction("Index", "Backgrounds");
            }
            return View(backgroundViewModel);
        }

        [Authorize(Roles = "Admin")]
        public void ChangeStatus(int? id)
        {
            var background = this.dataContext.Backgrounds.Where(nsb => nsb.Id == id.Value).First();

            background.EstablishedPicture = true;

            this.dataContext.Backgrounds.Update(background);

            var notSelectedBackgrounds = this.dataContext.Backgrounds.Where(nsb => nsb.Id != id.Value).ToList();
            foreach (var nsb in notSelectedBackgrounds)
            {
                nsb.EstablishedPicture = false;
                this.dataContext.Backgrounds.Update(nsb);
            }

            this.dataContext.SaveChanges();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await dataContext.Backgrounds.FirstOrDefaultAsync(b=>b.Id==id.Value);
            if (background == null)
            {
                return NotFound();
            }

            return View(background);
        }

        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await dataContext.Backgrounds.FindAsync(id);

            if (background.EstablishedPicture)
            {
                var newBackground = await this.dataContext.Backgrounds.FirstOrDefaultAsync(nb => nb.Id != id);
                if (newBackground != null)
                {
                    newBackground.EstablishedPicture = true;
                    this.dataContext.Backgrounds.Update(newBackground);
                }
            }

            imageHelper.DeleteImage(background.PhotoUrl);
            dataContext.Backgrounds.Remove(background);
            await dataContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
