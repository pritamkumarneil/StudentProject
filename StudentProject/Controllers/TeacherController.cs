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
        [HttpGet("teachers-from-standardId")]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetTeachersByStandardId(int id)
        {
            try
            {
                List<TeacherResponseDto> teachers = teacherService.GetAllTeacherOfGivenStandard(id);
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
    }
}
