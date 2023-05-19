using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Models;

namespace StudentProject.Transformer
{
    public static class TeacherTransformer
    {
        public static Teacher TeacherRequestDtoToTeacher(TeacherRequestDto teacherRequestDto)
        {
            Teacher teacher = new Teacher();
            teacher.TeacherName= teacherRequestDto.TeacherName;
            teacher.Age = teacherRequestDto.Age.ToString();
            teacher.EmailId = teacherRequestDto.EmailId;
            teacher.MobNo = teacherRequestDto.MobNo;
            teacher.Subject = teacherRequestDto.Subject;
            return teacher;
        }
        public static TeacherResponseDto TeacherToTeacherResponseDto(Teacher teacher)
        {
            TeacherResponseDto teacherResponseDto=new TeacherResponseDto();
            teacherResponseDto.TeacherName=teacher.TeacherName;
            teacherResponseDto.StandardName = teacher.standard==null?" ":teacher.standard.StandardName;
            teacherResponseDto.Sbuject = teacher.Subject!;

            return teacherResponseDto;
        }
    }
}
