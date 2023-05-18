using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Service;

namespace StudentProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentAddressController : ControllerBase
    {
        private readonly IStudentAddressService addressService;
        public StudentAddressController(IStudentAddressService studentAddressService)
        {
            this.addressService = studentAddressService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<AddressResponseDto>> AddStudentAddress(AddressRequestDto addressRequestDto)
        {
            try
            {
                AddressResponseDto address = addressService.AddAddress(addressRequestDto);
                return Ok(address);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAddresses")]
        public async Task<ActionResult<AddressResponseDto>> GetAddresses()
        {
            try
            {
                List<AddressResponseDto> addresses = addressService.GetAddresses();
                return Ok(addresses);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAddressWithPincode")]
        public async Task<ActionResult<IEnumerable<AddressResponseDto>>> GetAddressWithPinCode(int pin)
        {
            try
            {
                List<AddressResponseDto> addresses = addressService.GetAddressWithPinCode(pin);
                return Ok(addresses);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("getAllStudentsFromCity")]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAllStudentFromCity(string city)
        {
            try
            {
                List<StudentResponseDto> students = addressService.GetAllStudentFromCity(city);
                return Ok(students);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
