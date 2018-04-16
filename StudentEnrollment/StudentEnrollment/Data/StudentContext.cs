using Microsoft.EntityFrameworkCore;

namespace StudentEnrollment.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}