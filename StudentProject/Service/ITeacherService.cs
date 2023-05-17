using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;

namespace StudentProject.Service
{
    public interface ITeacherService
    {
        public TeacherResponseDto AddTeacher(TeacherRequestDto request);
        public TeacherResponseDto GetTeacherById(int id);
        public List<TeacherResponseDto> GetAllTeacher();
        public List<TeacherResponseDto> GetAllTeacherOfGivenStandard(int standardId);
        // add teacher to given standard
        public string AddTeacherToStandard(string StandardName,int teacherId);
       
    }
}
