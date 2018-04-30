using System;
using Xunit;
using StudentEnrollment;
using StudentEnrollment.Models;
using StudentEnrollment.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Controllers;
using System.Threading.Tasks;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void CanCreateCourse()
        {
            Course c = new Course() { Department = "CPTS", Description = "Awesome", Instructor = "OFallon", Level = 121 };
            Assert.Equal("OFallon", c.Instructor);
        }

        [Fact]
        public void CanConstructStudent()
        {
            Student s = new Student() { LastName = "Scott", FirstName = "Was Here", Enrolled = 4 };
            Assert.Equal(4, s.Enrolled);
        }

        [Fact]
        public async Task CanGetStudents()
        {
            var options = new DbContextOptionsBuilder<StudentContext>()
                .UseInMemoryDatabase(databaseName: "testing321")
                .Options;
            using (var context = new StudentContext(options))
            {
                var SController = new StudentController(context);
                Course c = new Course() { Department = "CPTS", Description = "Awesome", Instructor = "OFallon", Level = 121 };
                await context.Course.AddAsync(c);
                await context.SaveChangesAsync();
                var course = await context.Course.FirstOrDefaultAsync(x => x.Department == "CPTS");
                Student s1 = new Student() { LastName = "Scott", FirstName = "Was Here", Enrolled = course.ID };
                Student s2 = new Student() { LastName = "Steve", FirstName = "Was Not Here", Enrolled = course.ID };
                await context.Student.AddAsync(s1);
                await context.Student.AddAsync(s2);

                //Act
                var result = SController.Index();

                //Assert

                Assert.NotNull(result);
            }
        }

        //[Fact]
        //public async Task CanDeleteStudents()
        //{
        //    var options = new DbContextOptionsBuilder<StudentContext>()
        //        .UseInMemoryDatabase(databaseName: "testing321")
        //        .Options;
        //    using (var context = new StudentContext(options))
        //    {
        //        var SController = new StudentController(context);
        //        await context.SaveChangesAsync();
        //        var course = await context.Course.FirstOrDefaultAsync(x => x.Department == "CPTS");
        //        Student s1 = new Student() { LastName = "Scott", FirstName = "Was Here", Enrolled = 12 };
        //        Student s2 = new Student() { LastName = "Steve", FirstName = "Was Not Here", Enrolled = 12 };
        //        await context.Student.AddAsync(s1);
        //        await context.Student.AddAsync(s2);

        //        var list = await context.Student.ToListAsync();
        //        //Act
        //        var scott = await context.Student.FirstOrDefaultAsync(x => x.LastName == "Scott");
        //        SController.Delete(scott.ID);

        //        //Assert
        //        var numStudents = await context.Student.CountAsync();
        //        Assert.Equal(1, numStudents);
        //    }
        //}

        //[Fact]
        //public async Task CanCreateStudent()
        //{
        //    var options = new DbContextOptionsBuilder<StudentContext>()
        //        .UseInMemoryDatabase(databaseName: "testing321")
        //        .Options;
        //    using (var context = new StudentContext(options))
        //    {
        //        var SController = new StudentController(context);
        //        Course c = new Course() { Department = "CPTS", Description = "Awesome", Instructor = "OFallon", Level = 121 };
        //        await context.Course.AddAsync(c);
        //        await context.SaveChangesAsync();
        //        var course = await context.Course.FirstOrDefaultAsync(x => x.Department == "CPTS");
        //        Student s1 = new Student() { LastName = "Scott", FirstName = "Was Here", Enrolled = course.ID };
        //        Student s2 = new Student() { LastName = "Steve", FirstName = "Was Not Here", Enrolled = course.ID };
        //        await context.Student.AddAsync(s1);
        //        await context.Student.AddAsync(s2);

        //        var num = await context.Student.LongCountAsync();
        //        Assert.Equal(2, num);

        //        StudentViewModel newStudent = new StudentViewModel
        //        {
        //            FirstName = "Jason",
        //            LastName = "Bourne",
        //            CourseDepartment = "CPTS",
        //            CourseNumber = 121
        //        };

        //        //Act
        //        await SController.Create(newStudent);

        //        //Assert
        //        var numStudents = await context.Student.CountAsync();
        //        Assert.Equal(3, numStudents);
        //        var result = await context.Student.SingleOrDefaultAsync(x => x.FirstName == "Jason");
        //        Assert.NotNull(result);
        //    }
        //}
    }
}
