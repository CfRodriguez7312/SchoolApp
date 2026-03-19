using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Domain.Entities
{

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public bool Status { get; set; }

        public Rol Rol { get; set; }
    }

}
