using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;

namespace StudentProject.Service
{
    public interface IStudentAddressService
    {
        public AddressResponseDto AddAddress(AddressRequestDto addressRequestDto);
        public List<AddressResponseDto> GetAddresses();
        public List<AddressResponseDto> GetAddressWithPinCode(int pinCode);
        public List<StudentResponseDto> GetAllStudentFromCity(string city);
    }
}
