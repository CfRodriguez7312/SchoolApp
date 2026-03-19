using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Domain.Entities
{

    public class Rol
    {
        public int RolId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public List<User> Users { get; set; }
    }

}
