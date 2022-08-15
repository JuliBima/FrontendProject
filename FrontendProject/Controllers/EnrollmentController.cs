using FrontendProject.Models;
using FrontendProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontendProject.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollment _enrollment;
        private readonly ICourse _course;
        private readonly IStudent _student;

        public EnrollmentController(IEnrollment enrollment, ICourse course, IStudent student)
        {
            _enrollment = enrollment;
            _course = course;
            _student = student;
        }
        public async Task<IActionResult> Index(int? skip, int? take)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }

            IEnumerable<Enrollment> model;
            if (skip == null && take == null)
            {
                model = await _enrollment.GetAll(myToken);
            }
            else
            {
                model = await _enrollment.Pagging(skip,take,myToken);
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
            var model = await _enrollment.GetById(id, myToken);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            //Kirim Data Untuk Select Menu
            ViewBag.Course = new SelectList(await _course.GetAll(myToken), "CourseID", "Title");
            ViewBag.Student = new SelectList(await _student.GetAll(myToken), "ID", "FirstMidName");
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

            //Kirim Data Untuk Select Menu
            ViewBag.Course = new SelectList(await _course.GetAll(myToken), "CourseID", "Title");
            ViewBag.Student = new SelectList(await _student.GetAll(myToken), "ID", "FirstMidName");

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
