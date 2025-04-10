using CookieAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookieAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.UserName = User?.Identity?.Name;
          
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Admin()
        {
            return View();
        }


        [Authorize(Policy = "Premium")]
        public IActionResult Premium()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}