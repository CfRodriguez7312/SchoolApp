using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Domain.Entities
{

    public class Result
    {
        public int ResultId { get; set; }
        public int Assignment { get; set; }
        public int Value { get; set; }
        public bool Status { get; set; }

        public Assignment AssignmentNav { get; set; }
    }

}
