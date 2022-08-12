using FrontendProject.Models;
using FrontendProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendProject.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollment _enrollment;

        public EnrollmentController(IEnrollment enrollment)
        {
            _enrollment = enrollment;
        }
        public async Task<IActionResult> Index()
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            var model = await _enrollment.GetAll(myToken);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _enrollment.GetById(id, myToken);
            return View(model);
        }

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Enrollment course)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                var result = await _enrollment.Insert(course,myToken);
                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil menambahkan data {result.Grade} </div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["pesan"] =
                    $"<div class='alert alert-warning alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Gagal menambahkan data {ex.Message}</div>";
                return View();
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _enrollment.GetById(id,myToken);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Enrollment enrollment)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                var result = await _enrollment.Update(enrollment,myToken);
                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil merubah data {result.Grade} </div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _enrollment.GetById(id,myToken);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                await _enrollment.Delete(id,myToken);
                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil mendelete data dengan id: {id}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
