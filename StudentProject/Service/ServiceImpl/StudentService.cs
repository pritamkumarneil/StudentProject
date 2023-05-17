using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Exceptions;
using StudentProject.Models;
using StudentProject.Repository;
using StudentProject.Transformer;

namespace StudentProject.Service.ServiceImpl
{
    public class StudentService : IStudentService
    {
        private  SchoolDbContext schoolDbContext;
        public StudentService(SchoolDbContext schoolDbContext)
        {
            this.schoolDbContext = schoolDbContext;
        }

        /* public StudentService()
         {
         }*/

        // Getting Student repo

        public StudentResponseDto AddStudent(StudentRequestDto studentRequestDto)
        {
            // first fetch the Standard and also take standardName input
            // in order to give him a unique roll no

            // also add this student int  STudentCourse Table

            string standardName=studentRequestDto.StandardName;
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName + ";";
            Standard standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).First();
            if (standard == null)
                throw new StandardNotFoundException("Standard Not found");

            int rollNo = standard.Students.Count;

            Student student = StudentTransformer.StudentRequestDtoToStudent(studentRequestDto);

            student.RollNo = rollNo + 1;
            schoolDbContext.Students.Add(student);
            schoolDbContext.SaveChangesAsync();
            return StudentTransformer.StudentToStudentResponseDto(student);
        }

        public StudentResponseDto GetStudent(int id)
        {
            if (schoolDbContext.Students == null)
            {
                StudentResponseDto studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;
            }
            Student student = schoolDbContext.Students.Find(id);

            if (student == null)
            {
                StudentResponseDto studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;
            }

            return StudentTransformer.StudentToStudentResponseDto(student);
        }
        public StudentResponseDto UpdateStudent(int id, StudentRequestDto studentRequestDto)
        {
            StudentResponseDto studentResponseDto;
            if (schoolDbContext.Students == null)
            {
                studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;

            }
            Student student = schoolDbContext.Students.Find(id);

            if (student == null)
            {
                studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;
            }
            string oldName = student.Name;
            student.Name = studentRequestDto.Name.Equals("") ? student.Name : studentRequestDto.Name;
            student.Age = studentRequestDto.Age == 0 ? student.Age : studentRequestDto.Age;
            student.EmailId = studentRequestDto.EmailId == string.Empty ? student.EmailId : studentRequestDto.EmailId;
            student.MobNo = studentRequestDto.MobNo == string.Empty ? student.MobNo : studentRequestDto.MobNo;
            schoolDbContext.Students.Update(student);
            schoolDbContext.SaveChanges();
            studentResponseDto = StudentTransformer.StudentToStudentResponseDto(student);
            studentResponseDto.message = oldName + " updated to " + student.Name + "Succesfully";
            return studentResponseDto;

        }

        public List<StudentResponseDto> GetAllStudents()
        {
            List<StudentResponseDto> ans = new List<StudentResponseDto>();


            if (schoolDbContext.Students == null)
            {
                return new List<StudentResponseDto>();
            }
            List<Student> students = schoolDbContext.Students.ToList();
            if (students == null)
            {
                return new List<StudentResponseDto>();
            }
            foreach (Student student in students)
            {
                StudentResponseDto studentResponseDto = StudentTransformer.StudentToStudentResponseDto(student);
                ans.Add(studentResponseDto);
            }
            return ans;

        }

        public StudentResponseDto DeleteStudent(int id)
        {
            DbSet<Student> studentRepository = schoolDbContext.Students;
            StudentResponseDto ans;
            if (studentRepository == null)
            {
                ans = new StudentResponseDto();
                ans.message = "Invalid Id";
                return ans;
            }
            Student student = studentRepository.Find(id);
            if (student == null)
            {
                ans = new StudentResponseDto();
                ans.message = "Invalid Id";
                return ans;
            }
            studentRepository.Remove(student);
            schoolDbContext.SaveChanges();
            ans = new StudentResponseDto();
            ans.message = student.Name + " has been removed Successfully";
            return ans;
        }

        public string AddStudentToAddress(int addressId, int studentId)
        {
            if (schoolDbContext.Students == null)
                throw new StudentNotFoundException("Add student first");
            if (schoolDbContext.StudentAddresses == null)
                throw new StudentAddressNotFound("Add Address first");

            StudentAddress address = schoolDbContext.StudentAddresses.Find(addressId);
            if (address == null)
                throw new StudentAddressNotFound("Address Not found with given id "+addressId);
            Student student = schoolDbContext.Students.Find(studentId);
            if (student == null)
                throw new StudentNotFoundException("no student availabe with given id " + studentId);
            address.student = student;
            student.studentAddress = address;
            schoolDbContext.Students.Update(student);
            schoolDbContext.SaveChanges();

            return student.Name + " belongs to " + address.City;
        }
        public string AddStudentToCourse(string standardName, int rollNo, string CourseName)
        {
           // find standard
           // find student from the list having given roll no
           // find course from course name
           // then add student 
        }

        public List<StudentResponseDto> GetAllStudentFromCity(string city)
        {
            throw new NotImplementedException();
        }

        public List<CourseResponseDto> GetAllCoursesByStudent(string emailId)
        {
            throw new NotImplementedException();
        }
    }
}
