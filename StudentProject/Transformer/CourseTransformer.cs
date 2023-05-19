using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Models;

namespace StudentProject.Transformer
{
    public static class CourseTransformer
    {
        public static Course CourseRequestDtoToCourse(CourseRequestDto courseRequestDto)
        {
            Course course = new Course();
            course.CourseName = courseRequestDto.CourseName;
            course.Location = courseRequestDto.Location;
            return course;
        }
        public static CourseResponseDto CourseToCourseResponseDto(Course course)
        {
            CourseResponseDto courseResponse=new CourseResponseDto();

            courseResponse.CourseName = course.CourseName!;
            courseResponse.TeacherName = course.teacher==null?" ":course.teacher.TeacherName;
            return courseResponse;
        }
    }
}
