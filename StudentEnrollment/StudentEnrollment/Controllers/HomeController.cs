using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Models;
using StudentEnrollment.Models.ViewModels;

namespace StudentEnrollment.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentContext _context;

        // GET: /<controller>/
        public HomeController (StudentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<StudentViewModel> Students = from s in _context.Student
                           join c in _context.Course on s.Enrolled equals c.ID
                           orderby s.LastName
                           select new StudentViewModel
                           {
                               ID = s.ID,
                               LastName = s.LastName,
                               FirstName = s.FirstName,
                               CourseDeptartment = c.Department,
                               CourseNumber = c.Level
                           };
                                    
            return View(Students.ToList());
        }
    }
}
