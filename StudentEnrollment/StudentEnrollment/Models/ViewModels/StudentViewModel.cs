using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models.ViewModels
{
    public class StudentViewModel
    {
        // When student is displayed, join with course on ID and get this result
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(4)]
        [MinLength(3)]
        public string CourseDepartment { get; set; }

        [Required]
        [Range(100, 999)]
        public int CourseNumber { get; set; }
    }
}
