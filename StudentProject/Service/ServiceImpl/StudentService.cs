using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Models;
using StudentProject.Repository;
using StudentProject.Transformer;

namespace StudentProject.Service.ServiceImpl
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext schoolDbContext;
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

            Student student = StudentTransformer.StudentRequestDtoToStudent(studentRequestDto);
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
    }
}
