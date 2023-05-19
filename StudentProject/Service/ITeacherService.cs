using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;

namespace StudentProject.Service
{
    public interface ITeacherService
    {
        public TeacherResponseDto AddTeacher(TeacherRequestDto request);
        public TeacherResponseDto GetTeacherById(int id);
        public List<TeacherResponseDto> GetAllTeacher();
        public List<TeacherResponseDto> GetAllTeacherOfGivenStandard(string standardName);
        // add teacher to given standard
        public string AddTeacherToStandard(string StandardName,int teacherId);
        //get all students taught by teacher email id
        public List<StudentResponseDto> getAllStudentsTaughtByTeacher(string emailId);
       
    }
}
