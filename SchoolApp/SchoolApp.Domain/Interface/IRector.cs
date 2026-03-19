using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Domain.Interface
{
    public interface IRector
    {
        List<UserInfo> GetUserList(int rolid);
        bool SaveStudents(List<UserInfo> students);
        List<CourseInfo> GetCourseList();
        bool SaveCourses(List<CourseInfo> courses);
    }
}
