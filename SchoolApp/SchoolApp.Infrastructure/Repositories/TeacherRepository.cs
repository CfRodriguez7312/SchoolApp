using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interface;
using SchoolApp.Domain.Models;

namespace SchoolApp.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacher
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public List<CourseInfo> GetCoursesByTeacher(int teacherId)
        {
            var courses = _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .Select(c => new CourseInfo
                {
                    Courseid = c.CourseId,
                    Coursename = c.Name,
                    Teacherid = c.TeacherId,
                    Average = 0
                })
                .ToList();

            var averages = (
                from a in _context.Assignments
                join r in _context.Results
                    on a.AssignmentId equals r.Assignment
                where a.Status == true
                group r by a.CourseId into g
                select new
                {
                    CourseId = g.Key,
                    Avg = (int)Math.Round(g.Average(x => x.Value))
                }
            ).ToList();

            foreach (var c in courses)
            {
                var avg = averages.FirstOrDefault(x => x.CourseId == c.Courseid);
                c.Average = avg?.Avg ?? 0;
            }

            return courses;
        }

        public List<UserInfo> GetStudentsAssigned(int courseId)
        {
            var list =
                from a in _context.Assignments
                join u in _context.Users on a.StudentId equals u.UserId
                where a.CourseId == courseId && u.RolId == 2
                select new UserInfo
                {
                    Userid = u.UserId,
                    Name = u.Name,
                    Username = u.Username,
                    Rolid = u.RolId,
                    Status = u.Status
                };

            return list.ToList();
        }

        public List<UserInfo> GetStudentsNotAssigned(int courseId)
        {
            var assignedIds = _context.Assignments
                .Where(a => a.CourseId == courseId)
                .Select(a => a.StudentId)
                .ToList();

            var list = _context.Users
                .Where(u => u.RolId == 2 && !assignedIds.Contains(u.UserId))
                .Select(u => new UserInfo
                {
                    Userid = u.UserId,
                    Name = u.Name,
                    Username = u.Username,
                    Rolid = u.RolId,
                    Status = u.Status
                })
                .ToList();

            return list;
        }

        public bool SaveAssignments(int courseId, List<int> newStudentIds)
        {
            foreach (var id in newStudentIds)
            {
                _context.Assignments.Add(new Assignment
                {
                    CourseId = courseId,
                    StudentId = id,
                    Status = true
                });
            }

            _context.SaveChanges();
            return true;
        }

        public List<Result> GetStudentResults(int courseId, int studentId)
        {
            var results =
                from a in _context.Assignments
                join r in _context.Results
                    on a.AssignmentId equals r.Assignment into resultGroup
                from rg in resultGroup.DefaultIfEmpty()
                where a.CourseId == courseId && a.StudentId == studentId
                select new Result
                {
                    ResultId = rg != null ? rg.ResultId : 0,
                    Assignment = a.AssignmentId,
                    Value = rg != null ? rg.Value : 0,
                    Status = rg != null ? rg.Status : true
                };

            return results.ToList();
        }

        public bool SaveResults(int courseId, int studentId, List<ResultValue> results)
        {
            var assignments = _context.Assignments
                .Where(a => a.CourseId == courseId && a.StudentId == studentId)
                .ToList();

            foreach (var res in results)
            {
                if (res.ResultId == 0)
                {
                    _context.Results.Add(new Result
                    {
                        Assignment = assignments.First().AssignmentId,
                        Value = res.Value,
                        Status = true
                    });
                }
                else
                {
                    var r = _context.Results.Find(res.ResultId);
                    if (r != null)
                    {
                        r.Value = res.Value;
                    }
                }
            }

            _context.SaveChanges();
            return true;
        }

        public bool DeleteResult(int resultId)
        {
            var result = _context.Results.Find(resultId);
            if (result == null)
                return false;

            _context.Results.Remove(result);
            _context.SaveChanges();
            return true;
        }
    }
}