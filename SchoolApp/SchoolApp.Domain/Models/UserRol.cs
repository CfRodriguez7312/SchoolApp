using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Domain.Models
{
    public class UserRol
    {
        public int UserId { get; set; }
        public int RolId { get; set; }
        public string Name { get; set; } = null!;
        public string RolName { get; set; } = null!;
    }
}
