using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Domain.Models
{
    public class UserInfo
    {
        public int Userid { get; set; }
        public string Name { get; set; } = null!;
        public string Username{ get; set; } = null!;
        public int Rolid { get; set; }
        public bool Status { get; set; }
    }
}
