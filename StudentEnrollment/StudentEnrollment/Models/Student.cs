using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace StudentEnrollment.Models
{
    public class Student
    {
        public int ID { get; set; }

        [StringLength(30)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(45)]
        [Required]
        public string LastName { get; set; }

        // Foriegn key.
        [Required]
        public int Enrolled { get; set; }
    }
}
