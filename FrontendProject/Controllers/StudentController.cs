
﻿using FrontendProject.Models;
using FrontendProject.Services;

﻿using FrontendProject.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index(string? fristName, string? lastName , int? skip , int? take)
        {

            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }

            IEnumerable<Student> model;
            if(skip ==  null && take == null)
            {
                if (fristName == null && lastName == null)
                {

                    model = await _student.GetAll(myToken);
                }
                else if (fristName == null)
                {
                    model = await _student.GetByLastName(lastName, myToken);
                }
                else if (lastName == null)
                {
                    model = await _student.GetByFristMidName(fristName, myToken);
                }
                else
                {
                    model = await _student.GetByName(fristName, lastName, myToken);
                }
            }
            else
            {
                model = await _student.Pagging(skip, take,myToken);
            }
            

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            
            return View(model);
            
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetById(id, myToken);
            
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(Student student)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                var result = await _student.Insert(student,myToken);
                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil menambahkan data {result.FirstMidName} {result.LastName}</div>";
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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetById(id,myToken);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Student student)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                var result = await _student.Update(student,myToken);
                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    $"</button> Berhasil merubah data {result.FirstMidName} {result.LastName}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetById(id,myToken);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePost(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            try
            {
                await _student.Delete(id,myToken);
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

        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> StudentEnrollmentCourse()
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetEnrollmentCourses(myToken);

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View(model);
        }

        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> StudentWithCourse()
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetEnrollmentCourses(myToken);

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View(model);
        }




    }
}
