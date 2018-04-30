using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Models;
using StudentEnrollment.Models.ViewModels;




namespace StudentEnrollment.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        public StudentController (StudentContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Details( int id)
        {
            var vm = from s in _context.Student
                     join c in _context.Course on s.Enrolled equals c.ID
                     where s.ID == id
                     select new StudentViewModel
                     {
                         ID = s.ID,
                         LastName = s.LastName,
                         FirstName = s.FirstName,
                         CourseDepartment = c.Department,
                         CourseNumber = c.Level
                     };
            if (vm.FirstOrDefault() == null)
                return Redirect("~/Err");
                                                                  
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = from s in _context.Student
                     join c in _context.Course on s.Enrolled equals c.ID
                     select new StudentViewModel
                     {
                         ID = s.ID,
                         LastName = s.LastName,
                         FirstName = s.FirstName,
                         CourseDepartment = c.Department,
                         CourseNumber = c.Level
                     };
            if (vm.FirstOrDefault() == null)
                return Redirect("~/Err");

            return View(vm.ToList());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create ([Bind("FirstName,LastName,CourseDepartment,CourseNumber")] StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                var courseNum = GetCourseId(student.CourseDepartment, student.CourseNumber);
                if (courseNum != null)
                {
                    Student newStudent = new Student()
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Enrolled = (int)courseNum
                    };
                    _context.Student.Add(newStudent);
                    _context.Course.First(x => x.ID == newStudent.Enrolled).Count++;
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("Create", newStudent);
                }
            }
            return Redirect("~/Err");
        }

        public int? GetCourseId(string dept, int num)
        {
            return _context.Course.Where(x => x.Level == num)
                                  .FirstOrDefault(x => x.Department.ToUpper() == dept.ToUpper())
                                  .ID;
        }
    }
}