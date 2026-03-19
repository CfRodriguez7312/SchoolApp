using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.DTO;
using SchoolApp.Domain.Interface;
using SchoolApp.Domain.Models;

namespace SchoolApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RectorController : ControllerBase
    {
        private readonly IRector _rectorRepository;

        public RectorController(IRector rectorRepository)
        {
            _rectorRepository = rectorRepository;
        }

        [HttpGet("userlist/{rolid}")]
        public IActionResult GetUserList(int rolid)
        {
            var listuser = _rectorRepository.GetUserList(rolid);

            if (listuser.Count == 0)

                return NotFound("Usuarios no encontrados");


            return Ok(listuser);
        }

        [HttpPost("savestudents")]
        public IActionResult SaveStudents(List<UserInfo> students)
        {
            var result = _rectorRepository.SaveStudents(students);
            return Ok(result);
        }


        [HttpGet("courselist")]
        public IActionResult GetCourseList()
        {
            var courses = _rectorRepository.GetCourseList();
            return Ok(courses);
        }

        [HttpPost("savecourses")]
        public IActionResult SaveCourses([FromBody] List<CourseInfo> courses)
        {
            var ok = _rectorRepository.SaveCourses(courses);

            if (!ok)
                return BadRequest("Error guardando cursos");

            return Ok(new { message = "Cambios guardados correctamente" });
        }


    }
}
