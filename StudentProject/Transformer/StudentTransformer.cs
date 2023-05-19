using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Models;

namespace StudentProject.Transformer
{
    public static class StudentTransformer
    {
        
        public static Student StudentRequestDtoToStudent(StudentRequestDto studentRequestDto)
        {
            Student student = new Student();
 
            student.Name=studentRequestDto.Name;
            student.StudentDateOfBirth = studentRequestDto.StudentDateOfBirth;
            student.Branch= studentRequestDto.Branch;
            student.EmailId= studentRequestDto.EmailId; 
            student.MobNo= studentRequestDto.MobNo;
            student.Age = studentRequestDto.Age;
            student.StudentDateOfBirth = studentRequestDto.StudentDateOfBirth;
            return student;
        }
        public static StudentResponseDto StudentToStudentResponseDto(Student student)
        {
            StudentResponseDto studentResponseDto = new StudentResponseDto();

            studentResponseDto.RollNo = student.RollNo;
            studentResponseDto.Name = student.Name!;
            studentResponseDto.StandardName = student.Standard==null? "":student.Standard.StandardName;

            return studentResponseDto;
        }
    }
}
