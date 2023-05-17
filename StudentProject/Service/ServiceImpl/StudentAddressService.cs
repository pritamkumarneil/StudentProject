using Microsoft.EntityFrameworkCore;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Exceptions;
using StudentProject.Models;
using StudentProject.Repository;
using StudentProject.Transformer;

namespace StudentProject.Service.ServiceImpl
{
    public class StudentAddressService:IStudentAddressService
    {
        private SchoolDbContext schoolDbContext;
        public StudentAddressService(SchoolDbContext schoolDbContext)
        {
            this.schoolDbContext = schoolDbContext;
        }

        public AddressResponseDto AddAddress(AddressRequestDto addressRequestDto)
        {
            StudentAddress address = AddressTransfomer.AddressRequestDtoToAddress(addressRequestDto);
            schoolDbContext.StudentAddresses.Add(address);
            schoolDbContext.SaveChanges();
            return AddressTransfomer.AddressToAddressResponseDto(address);
        }

        public List<AddressResponseDto> GetAddresses()
        {
            if (schoolDbContext.StudentAddresses == null)
                throw new AddressNotFound("No Address Available yet");
            List<StudentAddress> addresses = schoolDbContext.StudentAddresses.ToList();
            List<AddressResponseDto> ans = new List<AddressResponseDto>();
            foreach(StudentAddress address in addresses)
            {
                ans.Add(AddressTransfomer.AddressToAddressResponseDto(address));
            }
            return ans;

        }

        public List<AddressResponseDto> GetAddressWithPinCode(int pinCode)
        {
            if (schoolDbContext.StudentAddresses == null)
                throw new AddressNotFound("No Address Available Yet");

            string sqlQuery = "SELECT * FROM studentaddresses s WHERE s.PinCode="+pinCode.ToString()+";";
            List<StudentAddress> addresses = schoolDbContext.StudentAddresses.FromSqlRaw(sqlQuery).ToList();

            List<AddressResponseDto> ans = new List<AddressResponseDto>();
            foreach(StudentAddress address in addresses)
            {
                ans.Add(AddressTransfomer.AddressToAddressResponseDto(address));
            }
            return ans;
        }

        public List<StudentResponseDto> GetAllStudentFromCity(string city)
        {
            if (schoolDbContext.StudentAddresses == null)
                throw new AddressNotFound("No Address Available Yet");

            string sqlQuery = "SELECT * FROM studentaddresses s WHERE s.PinCode=" +city+ ";";
            List<StudentAddress> addresses = schoolDbContext.StudentAddresses.FromSqlRaw(sqlQuery).ToList();

            List<StudentResponseDto> ans = new List<StudentResponseDto>();
            foreach (StudentAddress address in addresses)
            {
                ans.Add(StudentTransformer.StudentToStudentResponseDto(address.student));
            }
            return ans;
        }
    }
}
