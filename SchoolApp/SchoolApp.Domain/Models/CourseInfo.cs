using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Domain.Models
{
    public class CourseInfo
    {
        public int Courseid { get; set; }
        public string Coursename { get; set; } = null!;
        public int Average { get; set; }
        public int Teacherid { get; set; }

    }
}
