using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Service;
using System.ComponentModel;

namespace StudentProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly IStudentService studentService;
        public StudentController(IStudentService studentService)
        {
            this.studentService=studentService;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<StudentResponseDto>> PostStudent(StudentRequestDto studentRequestDto)
        {
            try
            {
                StudentResponseDto studentResponseDto = studentService.AddStudent(studentRequestDto);
                //studentResponseDto.message = "Student Added Succesfully";
                return Ok(studentResponseDto);
            }
            catch(Exception e) { return BadRequest(e.Message); }
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAllStudents()
        {
            List<StudentResponseDto> studentResponseDtos = studentService.GetAllStudents();
            return Ok(studentResponseDtos);

        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetById(int id)
        {
            StudentResponseDto studentResponseDto= studentService.GetStudent(id);
            return Ok(studentResponseDto);
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<StudentResponseDto>> UpdateStudent(int id, StudentRequestDto studentRequestDto)
        {
            StudentResponseDto studentResponseDto= studentService.UpdateStudent(id, studentRequestDto);
            return Ok(studentResponseDto);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<StudentResponseDto>> DeleteStudent(int id)
        {
            StudentResponseDto studentResponseDto= studentService.DeleteStudent(id);
            return Ok(studentResponseDto);
        }
        // add student to given Addresss
        [HttpPut("add-student-to-address")]
        public async Task<ActionResult<string>> addStudentToAddress(int addressId,int studentId)
        {
            try
            {
                string ans = studentService.AddStudentToAddress(addressId, studentId);
                return Ok(ans);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // add student to given course
        [HttpPut("add-student-to-course")]
        public async Task<ActionResult<string>> AddStudentToCourse(string standardName, int rollNo, string CourseName)
        {
            try
            {
                string response = studentService.AddStudentToCourse(standardName, rollNo, CourseName);
                return Ok(response);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        // get all student from given City
        [HttpGet("get-all-student-from-city")]
        public  async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAllStudentFromCity(string city)
        {
            try
            {
                List<StudentResponseDto> students = studentService.GetAllStudentFromCity(city);
                return Ok(students);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        // get all courses opted by the student(by emailId)
        [HttpGet("get-all-courses-by-student")]

        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetAllCoursesByStudent(string emailId)
        {
            try
            {
                List<CourseResponseDto> courses = studentService.GetAllCoursesByStudent(emailId);
                return Ok(courses);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("getStudentsWhoIsDoingCourseByTeacher")]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetStudentWhoIsInStandardAndDoingCourseByTeacher(string standardName, string teacherName)
        {
            try
            {
                List<StudentResponseDto> students = studentService.GetStudentWhoIsInStandardAndDoingCourseByTeacher(standardName, teacherName);
                return Ok(students);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
