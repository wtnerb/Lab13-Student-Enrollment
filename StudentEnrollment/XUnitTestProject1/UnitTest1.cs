using System;
using Xunit;
using StudentEnrollment;
using StudentEnrollment.Models;
using StudentEnrollment.Models.ViewModels;

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
        public void CanCreateStudent()
        {
            Student s = new Student() { LastName = "Scott", FirstName = "Was Here", Enrolled = 4 };
            Assert.Equal(4, s.Enrolled);
        }
    }
}
