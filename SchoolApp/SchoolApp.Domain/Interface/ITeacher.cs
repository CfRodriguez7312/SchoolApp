using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Domain.Interface
{
    public interface ITeacher
    {

        List<CourseInfo> GetCoursesByTeacher(int teacherId);
        List<UserInfo> GetStudentsAssigned(int courseId);
        List<UserInfo> GetStudentsNotAssigned(int courseId);
        bool SaveAssignments(int courseId, List<int> newStudentIds);
        List<Result> GetStudentResults(int courseId, int studentId);
        bool SaveResults(int courseId, int studentId, List<ResultValue> results);
        bool DeleteResult(int resultId);


    }
}
