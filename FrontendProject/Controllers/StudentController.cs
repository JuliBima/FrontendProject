using FrontendProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _student;
        public StudentController(IStudent student)
        {
            _student = student;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _student.GetAll();

            //ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View(model);
            
        }
    }
}
