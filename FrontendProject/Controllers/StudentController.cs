
﻿using FrontendProject.Models;
using FrontendProject.Services;

﻿using FrontendProject.Services;

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

        public async Task<IActionResult> Index(string? fristName, string? lastName , int? skip , int? take)
        {
            
            IEnumerable<Student> model;
            if(skip ==  null && take == null)
            {
                if (fristName == null && lastName == null)
                {

                    model = await _student.GetAll();
                }
                else
                {
                    model = await _student.GetByName(fristName, lastName);
                }
            }
            else
            {
                model = await _student.Pagging(skip, take);
            }
            

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            
            return View(model);
            
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = await _student.GetById(id);
            
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                var result = await _student.Insert(student);
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

        public async Task<IActionResult> Update(int id)
        {
            var model = await _student.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            try
            {
                var result = await _student.Update(student);
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

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _student.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _student.Delete(id);
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

        public async Task<IActionResult> StudentEnrollmentCourse()
        {
            var model = await _student.GetEnrollmentCourses();

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View(model);
        }

        public async Task<IActionResult> StudentWithCourse()
        {
            var model = await _student.GetEnrollmentCourses();

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View(model);
        }




    }
}
