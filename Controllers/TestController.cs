using Microsoft.AspNetCore.Mvc;

namespace CookieAuth.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}