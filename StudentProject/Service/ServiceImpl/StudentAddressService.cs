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
            // without student detail we can not add studentadress int the database 
            // so first get the detail of student
            string standardName = addressRequestDto.standardName;
            int rollNo = addressRequestDto.StudentRollNo;
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName="+standardName;
            Standard standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).Include(x=>x.Students).FirstOrDefault();
            Student student = null;
            if(standard==null)
            {
                throw new StandardNotFoundException("Please Enter Valid StandardName");
            }
            List<Student> students = standard.Students.ToList();
            if (students.Count == 0)
                throw new Exception("student couldn't saved to standard list");
            foreach(Student student1 in students)
            {
                if (student1.RollNo == rollNo)
                {
                    student = student1;
                }
            }
            if (student == null)
            {
                throw new StudentNotFoundException("please enter valid roll No "+addressRequestDto.StudentRollNo);
            }

            StudentAddress address = AddressTransfomer.AddressRequestDtoToAddress(addressRequestDto);

            address.student = student;
            student.studentAddress = address;

            schoolDbContext.Students.Update(student);
            //schoolDbContext.StudentAddresses.Add(address);
            
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

            string sqlQuery = "SELECT * FROM studentaddresses s WHERE s.PinCode="+pinCode.ToString();
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

            string sqlQuery = "SELECT * FROM studentaddresses s WHERE s.PinCode=" +city;
            List<StudentAddress> addresses = schoolDbContext.StudentAddresses.FromSqlRaw(sqlQuery).Include(x => x.student).ToList();

            List<StudentResponseDto> ans = new List<StudentResponseDto>();
            foreach (StudentAddress address in addresses)
            {
                ans.Add(StudentTransformer.StudentToStudentResponseDto(address.student));
            }
            return ans;
        }
    }
}
