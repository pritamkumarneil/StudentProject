using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Service;

namespace StudentProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentService studentService;
        public StudentController(IStudentService studentService)
        {
            this.studentService=studentService;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<StudentResponseDto>> PostStudent(StudentRequestDto studentRequestDto)
        {
            StudentResponseDto studentResponseDto= studentService.AddStudent(studentRequestDto);
            studentResponseDto.message = "Student Added Succesfully";
            return Ok(studentResponseDto);
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
    }
}
