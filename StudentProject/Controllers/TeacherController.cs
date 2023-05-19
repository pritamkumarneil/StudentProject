using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Service;
using StudentProject.Service.ServiceImpl;

namespace StudentProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService=teacherService;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<TeacherResponseDto>> AddTeacher(TeacherRequestDto teacherRequestDto)
        {
            try
            {
                TeacherResponseDto teacher = teacherService.AddTeacher(teacherRequestDto);
                return Ok(teacher);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getTeacherById")]
        public async Task<ActionResult<TeacherResponseDto>> GetTeacherById(int id)
        {
            try
            {
                TeacherResponseDto teacher = teacherService.GetTeacherById(id);
                return Ok(teacher);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAllTeachers")]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetAllTeachers()
        {
            try
            {
                List<TeacherResponseDto> teachers = teacherService.GetAllTeacher();
                return Ok(teachers);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("teachers-from-standarName")]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetTeachersByStandardId(string standardName)
        {
            try
            {
                List<TeacherResponseDto> teachers = teacherService.GetAllTeacherOfGivenStandard(standardName);
                return Ok(teachers);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("add-teacher-to-standard")]
        public async Task<ActionResult<string>> AddTeacherToStandard(string standaradName, int teacherId)
        {
            try
            {
                string response = teacherService.AddTeacherToStandard(standaradName, teacherId);
                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("get-all-student-by-teacher-emailId")]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> getAllStudentsTaughtByTeacher(string emailId)
        {
            try
            {
                List<StudentResponseDto> students = teacherService.getAllStudentsTaughtByTeacher(emailId);
                return Ok(students);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
