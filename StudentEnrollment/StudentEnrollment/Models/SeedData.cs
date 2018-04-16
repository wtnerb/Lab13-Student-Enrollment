using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace StudentEnrollment.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudentContext(
                serviceProvider.GetRequiredService<DbContextOptions<StudentContext>>()))
            {
                if (context.Student.Any())
                {
                    return;
                }

                context.Course.AddRange(
                    new Course
                    {
                        Department = "LEAD",
                        ID = 2, // must set this so can be used by students as foriegn key
                        Level = 703,
                        Description = "Leadership at scale",
                        Instructor = "I dunno"
                    },
                    new Course
                    {
                        Department = "COMD",
                        ID = 3,
                        Level = 642,
                        Description = "How to be really funny without being (too) crude",
                        Instructor = "extensive practice"
                    }
                    );
                context.SaveChanges();

                context.Student.AddRange(
                    new Student
                    {
                        FirstName = "George",
                        LastName = "Washington",
                        Enrolled = 2, // Foriegn key for spy 101
                    },

                    new Student
                    {
                        FirstName = "Thomas",
                        LastName = "Jefferson",
                        Enrolled = 2,
                    },

                    new Student
                    {
                        FirstName = "Jay",
                        LastName = "Leno",
                        Enrolled = 3,
                    },

                    new Student
                    {
                        FirstName = "Bill",
                        LastName = "Gates",
                        Enrolled = 2,
                    });

                context.SaveChanges();
            }
        }
    }
}
