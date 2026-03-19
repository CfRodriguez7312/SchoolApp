using Microsoft.EntityFrameworkCore;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interface;
using SchoolApp.Domain.Models;

namespace SchoolApp.Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly SchoolDbContext _context;

        public UserRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<UserRol?> GetByCredentials(string username, string password)
        {
            var userRol = await _context.Users
                .Join(_context.Rols, u => u.RolId, r => r.RolId, (u, r) => new { u, r })
                .Where(x => x.u.Username == username
                                       && x.u.Password == password
                                       && x.u.Status == true)
                .Select(s => new UserRol
                {
                    UserId = s.u.UserId,
                    Name = s.u.Name,
                    RolId = s.r.RolId,
                    RolName = s.r.Name
                }).FirstOrDefaultAsync();

            return userRol;
        }
    }
}