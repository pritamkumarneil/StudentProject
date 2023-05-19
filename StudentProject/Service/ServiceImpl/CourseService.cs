using Microsoft.EntityFrameworkCore;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Exceptions;
using StudentProject.Models;
using StudentProject.Repository;
using StudentProject.Transformer;

namespace StudentProject.Service.ServiceImpl
{
    public class CourseService : ICourseService
    {
        private readonly SchoolDbContext schoolDbContext;
        public CourseService(SchoolDbContext schoolDb)
        {
            this.schoolDbContext = schoolDb;
        }
        public  CourseResponseDto AddCourse(CourseRequestDto courseRequestDto)
        {
            // while adding the course you should also update the StudentCourse table 

            int teacherId = courseRequestDto.TeacherId;
            Teacher? teacher = schoolDbContext.Teachers.Find(teacherId);
            if (teacher == null)
                throw new TeacherNotFoundException("Teacher Doesn't exist with given TeacherId");

            Course course = CourseTransformer.CourseRequestDtoToCourse(courseRequestDto);

            //add teacher in the course
            course.teacher = teacher;

            // add course in Teacher entity also 
            teacher.Courses.Add(course);
            schoolDbContext.Teachers.Update(teacher);
            schoolDbContext.SaveChanges();

            CourseResponseDto courseResponseDto= CourseTransformer.CourseToCourseResponseDto(course);
            courseResponseDto.Message = "Course Has Been Added Succesfully with above details ";
            return courseResponseDto;
        }

        public List<CourseResponseDto> GetAllCourses()
        {
            if (schoolDbContext.Courses == null)
                throw new CourseNotFound("No Course Available");
            List<Course> courses = schoolDbContext.Courses.Include(x=>x.teacher).ToList();
            List<CourseResponseDto> courseResponseDtos = new List<CourseResponseDto>();
            foreach (Course course in courses)
                courseResponseDtos.Add(CourseTransformer.CourseToCourseResponseDto(course));
            return courseResponseDtos;
            
        }

        public CourseResponseDto GetCourseById(int id)
        {
             if(schoolDbContext.Courses == null)
                throw new CourseNotFound("No Course Available");
            Course? course = schoolDbContext.Courses.Where(x=>x.CourseId==id).Include(x => x.teacher).FirstOrDefault();
            if (course == null)
                throw new CourseNotFound("No course Available with given Id");
            return CourseTransformer.CourseToCourseResponseDto(course);
        }

        public List<CourseResponseDto> GetAllCourseByTeacherId(int teacherId)
        {
            /*if (schoolDbContext.Courses == null)
            {
                throw new CourseNotFound("No Course Available");
            }
            Teacher teacher = schoolDbContext.Teachers.Find(teacherId);
            if (teacher == null)
            {
                throw new TeacherNotFoundException("Teacher Doesn't exist with given Teacher Id");
            }*/
            // this can be easily acheived by using teacher Dbset but here i just wanted to use 
            // raw sql query
            string sqlQuery = "SELECT * FROM courses c WHERE c.TeacherId=" + teacherId.ToString();
            List<Course> courses = schoolDbContext.Courses.FromSqlRaw(sqlQuery).Include(x=>x.teacher).ToList();
            List<CourseResponseDto> ans = new List<CourseResponseDto>();
            foreach(Course course in courses)
            {
                ans.Add(CourseTransformer.CourseToCourseResponseDto(course));
            }
            return ans;
        }

        public List<StudentResponseDto> GetAllStudentsByCourseName(string courseName)
        {
            string sqlQuery = "SELECT * FROM courses c WHERE c.CourseName='" + courseName + "'";
            Course? course = schoolDbContext.Courses.FromSqlRaw(sqlQuery).Include(x => x.StudentCourses).ThenInclude(sc => sc.student).FirstOrDefault();
            if (course == null)
            {
                throw new CourseNotFound("No course with name " + courseName + " found.");
            }
            List<StudentResponseDto> students = new List<StudentResponseDto>();
            foreach(StudentCourse courseByStudet in course.StudentCourses.ToList())
            {
                students.Add(StudentTransformer.StudentToStudentResponseDto(courseByStudet.student));
            }
            return students;
        }
    }
}
