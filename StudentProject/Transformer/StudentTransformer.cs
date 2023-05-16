using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Models;

namespace StudentProject.Transformer
{
    public class StudentTransformer
    {
        /*private static int Id { get; set; }=0;*/
        public static Student StudentRequestDtoToStudent(StudentRequestDto studentRequestDto)
        {
            Student student = new Student();
           /* student.Id =Id++;*/
            student.Name=studentRequestDto.Name;
            student.StudentDateOfBirth = studentRequestDto.StudentDateOfBirth;
            student.Branch= studentRequestDto.Branch;
            student.EmailId= studentRequestDto.EmailId; 
            student.MobNo= studentRequestDto.MobNo;
            student.Age = studentRequestDto.Age;
            student.RollNo = studentRequestDto.RollNo;
            student.StudentDateOfBirth = studentRequestDto.StudentDateOfBirth;
            return student;
        }
        public static StudentResponseDto StudentToStudentResponseDto(Student student)
        {
            StudentResponseDto studentResponseDto = new StudentResponseDto();

            studentResponseDto.RollNo = student.RollNo;
            studentResponseDto.Name = student.Name;

            return studentResponseDto;
        }
    }
}
