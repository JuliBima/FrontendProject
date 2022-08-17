using FrontendProject.Models;
using FrontendProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }
        public IActionResult Index()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            await _user.LoginAsync(user);
            TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil Login</div>";
            return RedirectToAction("Index");
        }
    }
}
