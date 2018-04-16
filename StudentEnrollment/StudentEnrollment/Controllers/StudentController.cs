using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Models;


namespace StudentEnrollment.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        // GET: /<controller>/
        public IActionResult Index()
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
    }

}
