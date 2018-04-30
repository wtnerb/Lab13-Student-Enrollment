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

        // GET: /<controller>/
        public CourseController (StudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

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

        public IActionResult Index()
        {                                 
            return View(_context.Course.Take(10).ToList());
        }

        public int? GetCourseId(string dept, int num)
        {
            return _context.Course.Where(x => x.Level == num)
                                  .FirstOrDefault(x => x.Department.ToUpper() == dept.ToUpper())
                                  .ID;
        }
    }
}
