using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models.ViewModels
{
    public class StudentViewModel
    {
        // When student is displayed, join with course on ID and get this result
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Enrolled { get; set; }

    }
}
