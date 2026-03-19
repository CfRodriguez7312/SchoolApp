using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interface;
using SchoolApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace SchoolApp.Infrastructure.Repositories
{
    public class RectorRepository : IRector
    {

        protected readonly SchoolDbContext _context;

        public RectorRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public List<UserInfo> GetUserList(int rolid)
        {
            var listStudent = _context.Users
                .Where(wh => wh.RolId == rolid)
                .Select(s => new UserInfo()
                {
                    Userid = s.UserId,
                    Name = s.Name,
                    Username = s.Username,
                    Rolid = s.RolId,
                    Status = s.Status

                }).ToList();

            return listStudent;
        }

        public bool SaveStudents(List<UserInfo> students)
        {
            foreach (var s in students)
            {
                if (s.Userid == 0)
                {
                    var password = GenerateRandomPassword();

                    _context.Users.Add(new User
                    {
                        Name = s.Name,
                        RolId = s.Rolid,
                        Username = s.Username,
                        Password = password,
                        Status = true
                    });
                }
                else
                {
                    var user = _context.Users.Find(s.Userid);

                    if (user == null)
                        return false;

                    user.Name = s.Name;
                    user.Username = s.Username;
                }
            }

            _context.SaveChanges();
            return true;
        }

        private static string GenerateRandomPassword(int length = 10)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=<>?";

            StringBuilder res = new StringBuilder();
            byte[] randomBytes = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            for (int i = 0; i < length; i++)
            {
                res.Append(valid[randomBytes[i] % valid.Length]);
            }

            return res.ToString();
        }


        public List<CourseInfo> GetCourseList()
        {
            var courses = _context.Courses
                .Select(c => new CourseInfo
                {
                    Courseid = c.CourseId,
                    Coursename = c.Name,
                    Teacherid = c.TeacherId,
                    Average = 0
                })
                .ToList();

            var averages = _context.Assignments
                .Where(a => a.Status == true)
                .Join(_context.Results,
                      a => a.AssignmentId,
                      r => r.Assignment,
                      (a, r) => new { a.CourseId, r.Value })
                .GroupBy(x => x.CourseId)
                .Select(g => new
                {
                    CourseId = g.Key,
                    Avg = (int)Math.Round(g.Average(x => x.Value))
                })
                .ToList();

            foreach (var c in courses)
            {
                var avg = averages.FirstOrDefault(a => a.CourseId == c.Courseid);
                c.Average = avg?.Avg ?? 0;
            }

            return courses;
        }

        public bool SaveCourses(List<CourseInfo> courses)
        {
            foreach (var c in courses)
            {
                if (c.Courseid == 0)
                {
                    var newCourse = new Course
                    {
                        Name = c.Coursename,
                        TeacherId = c.Teacherid,
                        Status = true
                    };

                    _context.Courses.Add(newCourse);
                }
                else
                {
                    var course = _context.Courses.Find(c.Courseid);

                    if (course == null)
                        return false;

                    course.Name = c.Coursename;
                    course.TeacherId = c.Teacherid;
                }
            }

            _context.SaveChanges();
            return true;
        }
    }


}
