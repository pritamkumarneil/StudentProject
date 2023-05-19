using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Service;

namespace StudentProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CourseResponseDto>> AddCourse(CourseRequestDto courseRequestDto)
        {
            try
            {
                CourseResponseDto course = courseService.AddCourse(courseRequestDto);
                return Ok(course);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAllCourse")]
        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetAllCourses()
        {
            try
            {
                List<CourseResponseDto> courses = courseService.GetAllCourses();
                return Ok(courses);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("getCourseById")]
        public async Task<ActionResult<CourseResponseDto>> GetCourseById(int id)
        {
            try
            {
                CourseResponseDto course= courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("getAllCourseByTeacher")]
        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetAllCourseByTeacherId(int teacherId)
        {
            try
            {
                List<CourseResponseDto> courses = courseService.GetAllCourseByTeacherId(teacherId);
                return Ok(courses);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("getAllStudentsByCourseName")]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAllStudentsByCourseName(string courseName)
        {
            try
            {
                List<StudentResponseDto> students = courseService.GetAllStudentsByCourseName(courseName);
                return Ok(students);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
