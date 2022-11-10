using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    PhotoUrl = await imageHelper.UploadImageAsync(backgroundViewModel.ImageFile, backgroundViewModel.Name, "BackgroundPictures")
                };
            }
            return View();
        }
    }
}
