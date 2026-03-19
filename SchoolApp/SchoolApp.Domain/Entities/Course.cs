using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Domain.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public bool Status { get; set; }

        public User Teacher { get; set; }
        public List<Assignment> Assignments { get; set; }
    }

}
