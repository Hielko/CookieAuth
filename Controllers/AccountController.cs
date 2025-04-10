using CookieAuth.Models;
using CookieAuth.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersRepo _usersRepo;

        public AccountController(IUsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserToLogin? userToLogin, string? ReturnUrl)
        {
            var user = _usersRepo?.FindUser(userToLogin?.UserName, userToLogin?.Password);

            if (user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim("FullName", user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("AccountType", user.AccountType),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //var authProperties = new AuthenticationProperties
                //{
                //    RedirectUri = ReturnUrl, //  "/Home/Index",
                //};

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                    );

                return Redirect(ReturnUrl ?? "/Home/Index"); 
            }

            ViewBag.Error = "Unknown user or wrong password";
            // return Redirect("/Account/Error");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account", new { ReturnUrl = "/"});
        }
    }

}