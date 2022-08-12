using FrontendProject.Models;
using FrontendProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontendProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourse _course;

        public CourseController(ICourse course)
        {
            _course = course;
        }

        public async Task<IActionResult> Index(string? title, int? skip, int? take)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            IEnumerable<Course> model;
            if (skip == null && take == null)
            {
                if (title == null)
                {

                    model = await _course.GetAll(myToken);
                }
                else
                {
                    model = await _course.GetByTitle(title, myToken);
                }
            }
            else
            {
                model = await _course.Pagging(skip, take, myToken);
            }

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {

            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _course.GetById(id,myToken);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                var result = await _course.Insert(course, myToken);
                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil menambahkan data {result.Title} </div>";
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
            var model = await _course.GetById(id,myToken);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course course)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                var result = await _course.Update(course,myToken);
                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil merubah data {result.Title} </div>";
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
            var model = await _course.GetById(id,myToken);
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
                await _course.Delete(id,myToken);
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

        public async Task<IActionResult> CourseWithStudent()
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }

            var model = await _course.GetWithStudent(myToken);

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View(model);
        }
    }
}
