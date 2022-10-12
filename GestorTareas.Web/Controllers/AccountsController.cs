using GestorTareas.Web.Data;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly DataContext dataContext;
      

        public AccountsController(IUserHelper userHelper, DataContext dataContext)
        {
            this.userHelper = userHelper;
            this.dataContext = dataContext;

        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)

                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await userHelper.LoginAsync(login.Email, login.Password, login.RememberMe);
                if (result.Succeeded)
                {
                    var user = await userHelper.GetUserByEmailAsync(login.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Error de inicio de sesión");
                return View(login);
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotAutorized()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            if (passwordViewModel.NewPassword == passwordViewModel.RepeatedNewPassword)
            {
                var user = await this.dataContext.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
                await this.userHelper.ChangePasswordAsync(user,passwordViewModel.CurrentPassword, passwordViewModel.NewPassword);
                await dataContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden. Inténtelo nuevamente");
                return View(passwordViewModel);

            }
        }

    }
}
