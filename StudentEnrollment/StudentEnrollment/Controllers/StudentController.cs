using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Models;


namespace StudentEnrollment.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        public IActionResult Index( int id)
        {
            var vm = from s in _context.Student
                     join c in _context.Course on s.Enrolled equals c.ID
                     where s.ID == id
                     select new Models.ViewModels.StudentViewModel
                     {
                         ID = s.ID,
                         LastName = s.LastName,
                         FirstName = s.FirstName,
                         Enrolled = c.Department + " " + c.Level
                     };
            if (vm.FirstOrDefault() == null)
                return Redirect("~/Err");
                                                                  
            return View(vm.FirstOrDefault());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create ([Bind("FirstName,LastName,Enrolled")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Student.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Redirect("~/Err");
        }

        public async Task<int?> GetCourseId(string dept, int num)
        {
            return _context.Course.Where(x => x.Level == num).FirstOrDefault(x => x.Department.ToUpper() == dept).ID;
        }
    }

}
