using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Models;
using StudentEnrollment.Models.ViewModels;

namespace StudentEnrollment.Controllers
{
    public class CourseController : Controller
    {
        private readonly StudentContext _context;

        public CourseController (StudentContext context)
        {
            _context = context;
        }

        //Create form
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //Adds submission from form to the db
        [HttpPost]
        public async Task<IActionResult> Create(Course Course)
        {
            if (ModelState.IsValid)
            {
                await _context.Course.AddAsync(Course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("~/Err");
        }

        //main page for course
        public IActionResult Index()
        {
            List<Course> courseList = _context.Course.Take(10).ToList();
            foreach(Course c in courseList)
            {
                c.Count = _context.Student.Where(x => x.Enrolled == c.ID).Count();
            }
            return View(courseList);
        }
    }
}
