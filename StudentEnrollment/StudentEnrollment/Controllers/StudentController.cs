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
                                                                  
            return View( vm.ToList());
        }

        //Main student page
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

        //Sets up form
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //Creates from form
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
                    await _context.SaveChangesAsync();
                    return Redirect("Index");
                }
            }
            return Redirect("~/Err");
        }

        //Not invoked put method.
        [HttpPut]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid)
            {
                if (_context.Student.Any(s => s.ID == student.ID))
                {
                    _context.Student.Update(student);
                    _context.SaveChanges();
                    return Redirect("~/Student/Index");
                }
            }
            return Redirect("~/Err");
        }

        //Deletes from database based upon provided id
        public IActionResult Delete (int id)
        {
            if (_context.Student.Any(s => s.ID == id))
            {
                Student s = _context.Student.First(student => student.ID == id);
                _context.Student.Remove(s);
                _context.SaveChanges();
                return Redirect("~/Student/Index");
            }
            return Redirect("~/Err");
        }

        //Helper function.
        public int? GetCourseId(string dept, int num)
        {
            Course c = _context.Course.Where(x => x.Level == num)
                                      .FirstOrDefault(x => x.Department.ToUpper() == dept.ToUpper());
            if (c != null)
            {
                return c.ID;
            }
            return null;
        }
    }
}