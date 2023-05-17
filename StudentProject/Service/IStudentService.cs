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
        // add student to given Addresss(addressId)
        public string AddStudentToAddress(int addressId,int studentId);
        // add student to given course
        public string AddStudentToCourse(string standardName,int rollNo, string CourseName);
        // get all student from given City
        public List<StudentResponseDto> GetAllStudentFromCity(string city);
        // get all courses opted by the student(by emailId)
        public List<CourseResponseDto> GetAllCoursesByStudent(string emailId);
    }
}
