using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;

namespace StudentProject.Service
{
    public interface IStandardService
    {
        //add standard
        public StandardResponseDto AddStandard(StandardRequestDto standardRequestDto);
        // get all standard
        public List<StandardResponseDto> GetStandards();
        // get All Student from StandardId/name
        public List<StudentResponseDto> GetAllStudentsFromStandard(string standardName);
        // get All Teachers from standardId/ name
        public List<TeacherResponseDto> GetAllTeachersFromStandard(string standardName);
        // add student to standard(standardName and studentid)
        /*public string AddStudentToStandard(string standardName, int studentId);*/
        // add Teacher to Standard(teacher mobNO,standardName)
        public string AddTeacherToStandard(string mobNo, string standardName);
    }
}
