using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySalonWeb.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MySalonWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        [AllowAnonymous]
        [Route("/account")]
        public IActionResult Account()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
            { return RedirectToAction("welcome"); }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        // [Route("login")]
        public async Task<IActionResult> Login(Account account)
        {

            if (account.Username == "admin" && account.Password == "123")
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, account.Username),
                    new Claim("OtherProperyies", "Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);


                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = account.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("welcome");
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        [Route("welcome")]
        public IActionResult Welcome()
        {
            return View("welcome");
        }

        [Route("orders")]
        public IActionResult Orders()
        {
            return View();
        }

        public async Task<IActionResult> Logout(Account account)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
