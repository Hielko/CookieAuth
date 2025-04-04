﻿using CookieAuth.Models;
using CookieAuth.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        [HttpPost]
        public async Task<IActionResult> Login(UserToLogin userToLogin, string ReturnUrl)
        {
            var user = _usersRepo.GetUsers().Find(c => c.UserName == userToLogin.UserName && c.Password == userToLogin.Password);

            if (user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userToLogin.UserName),
                    new Claim("FullName", userToLogin.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),
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

                return Redirect(ReturnUrl);
            }

            ViewBag.Error = "Unknown user or wrong password";
            // return Redirect("/Account/Error");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }

}