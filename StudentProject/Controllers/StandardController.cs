using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Service;

namespace StudentProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StandardController : ControllerBase
    {
        private readonly IStandardService standardService;

        public StandardController(IStandardService standardService)
        {
            this.standardService = standardService;
        }

        [HttpPut("Add")]
        public async Task<ActionResult<StandardResponseDto>> AddStandard(StandardRequestDto standardRequestDto)
        {
            try
            {
                StandardResponseDto standardResponseDto = standardService.AddStandard(standardRequestDto);
                return Ok(standardResponseDto);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<StandardResponseDto>>> getAllStandards()
        {
            try
            {
                List<StandardResponseDto> standards = standardService.GetStandards();
                return Ok(standards);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("getAllStudentInStandard")]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> getAllStudentsInStandard(string standardName)
        {
            try
            {
                List<StudentResponseDto> students = standardService.GetAllStudentsFromStandard(standardName);
                return Ok(students);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAllTeachersInStandard")]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetAllTeachersInStandard(string standardName)
        {
            try
            {
                List<TeacherResponseDto> teachers = standardService.GetAllTeachersFromStandard(standardName);
                return Ok(teachers);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("AddTeacherToStandard")]
        public async Task<ActionResult<string>> AddTeacherToStandard(string mobNo, string standardName)
        {
            try
            {
                return standardService.AddTeacherToStandard(mobNo, standardName);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
