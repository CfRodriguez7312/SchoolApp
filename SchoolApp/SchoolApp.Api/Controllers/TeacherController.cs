using Microsoft.AspNetCore.Mvc;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interface;
using SchoolApp.Domain.Models;
using System.Collections.Generic;

namespace SchoolApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacher _repo;

        public TeacherController(ITeacher repo)
        {
            _repo = repo;
        }

        [HttpGet("courses/{teacherId}")]
        public IActionResult GetCourses(int teacherId)
        {
            return Ok(_repo.GetCoursesByTeacher(teacherId));
        }

        [HttpGet("studentsassigned/{courseId}")]
        public IActionResult GetStudentsAssigned(int courseId)
        {
            return Ok(_repo.GetStudentsAssigned(courseId));
        }

        [HttpGet("studentsnotassigned/{courseId}")]
        public IActionResult GetStudentsNotAssigned(int courseId)
        {
            return Ok(_repo.GetStudentsNotAssigned(courseId));
        }

        [HttpPost("assign/{courseId}")]
        public IActionResult AssignStudents(int courseId, [FromBody] List<int> studentIds)
        {
            var ok = _repo.SaveAssignments(courseId, studentIds);
            if (!ok)
                return BadRequest("Error asignando estudiantes");

            return Ok(new { message = "Asignaciones guardadas" });
        }

        [HttpGet("results/{courseId}/{studentId}")]
        public IActionResult GetStudentResults(int courseId, int studentId)
        {
            return Ok(_repo.GetStudentResults(courseId, studentId));
        }

        [HttpPost("saveresults/{courseId}/{studentId}")]
        public IActionResult SaveResults(
            int courseId,
            int studentId,
            [FromBody] List<ResultValue> results)
        {
            var ok = _repo.SaveResults(courseId, studentId, results);
            return ok ? Ok() : BadRequest("Error guardando notas");
        }

        [HttpDelete("deleteresult/{resultId}")]
        public IActionResult DeleteResult(int resultId)
        {
            var ok = _repo.DeleteResult(resultId);
            return ok ? Ok() : BadRequest("No se pudo eliminar la nota");
        }
    }
}