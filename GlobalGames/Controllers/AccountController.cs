using GlobalGames.Helpers;
using GlobalGames.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGames.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;


        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Inscricoes");
            }

            return this.View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userHelper.LoginInAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("Contains"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Inscricoes");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
