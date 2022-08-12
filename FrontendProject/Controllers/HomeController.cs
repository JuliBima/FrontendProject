using FrontendProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                HttpContext.Session.SetString("token",
                    "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imp1bGliaW1hMTJAcmFwaWR0ZWNoLmlkIiwibmJmIjoxNjYwMjY3NjUzLCJleHAiOjE2NjE1NjM2NTIsImlhdCI6MTY2MDI2NzY1M30.YjvrRf0QczbX4oA1QPH7N7WBDzSVhU-S6ZAP-fryDgg");
            }

            return View();
        }

        public IActionResult Privacy()
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