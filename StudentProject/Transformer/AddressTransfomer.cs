using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Models;

namespace StudentProject.Transformer
{
    public static class AddressTransfomer
    {
        public static StudentAddress AddressRequestDtoToAddress(AddressRequestDto addressRequestDto)
        {
            StudentAddress address=new StudentAddress();
            address.Address1 = addressRequestDto.Address1;
            address.Address2 = addressRequestDto.Address2; 
            address.City = addressRequestDto.City;
            address.State = addressRequestDto.State;
            address.PinCode = addressRequestDto.PinCode;

            return address;
        }
        public static AddressResponseDto AddressToAddressResponseDto(StudentAddress address)
        {
            AddressResponseDto addressResponseDto = new AddressResponseDto();
            addressResponseDto.Address1 = address.Address1;
            addressResponseDto.Address2 = address.Address2;
            addressResponseDto.PinCode= address.PinCode;
            addressResponseDto.City = address.City;
            addressResponseDto.State = address.State;
            addressResponseDto.Message = "Successfully Updated";
            return addressResponseDto;
        }
    }
}
