using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Domain.Interface
{

    public interface IUser
    {
        Task<UserRol?> GetByCredentials(string username, string password);
    }

}
