using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;

namespace StudentProject.Service
{
    public interface IStudentService
    {
        public StudentResponseDto AddStudent(StudentRequestDto studentRequestDto);
        public StudentResponseDto GetStudent(int id);
        public List<StudentResponseDto> GetAllStudents();
        public StudentResponseDto UpdateStudent(int id,StudentRequestDto studentRequestDto);
        public StudentResponseDto DeleteStudent(int id);
    }
}
