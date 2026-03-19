using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Domain.Entities
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public bool Status { get; set; }

        public Course Course { get; set; }
        public User Student { get; set; }
        public List<Result> Results { get; set; }
    }
}
