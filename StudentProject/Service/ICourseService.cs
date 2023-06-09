﻿using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;

namespace StudentProject.Service
{
    public interface ICourseService
    {
        public CourseResponseDto AddCourse(CourseRequestDto courseRequestDto);
        public List<CourseResponseDto> GetAllCourses();
        public CourseResponseDto GetCourseById(int id);
        public List<CourseResponseDto> GetAllCourseByTeacherId(int teacherId);
        public List<StudentResponseDto> GetAllStudentsByCourseName(string courseName);
        // get all students who is from given city and doing given course
        public List<StudentResponseDto> GetAllStudentsFromCityDoingThisCourse(string city, string courseName);
    }
}
