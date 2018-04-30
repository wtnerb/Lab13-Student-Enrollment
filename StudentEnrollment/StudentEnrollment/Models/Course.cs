using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Models
{
    public class Course
    {
        [Required]
        public int ID { get; set; }
        
        [StringLength(4)]
        [Required]
        public string Department { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public string Instructor { get; set; }

        public string Description { get; set; }

        public uint Count = 0;
    }
}
