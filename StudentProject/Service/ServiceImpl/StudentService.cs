﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Exceptions;
using StudentProject.Models;
using StudentProject.Repository;
using StudentProject.Transformer;

namespace StudentProject.Service.ServiceImpl
{
    public class StudentService : IStudentService
    {
        private readonly  SchoolDbContext schoolDbContext;
        public StudentService(SchoolDbContext schoolDbContext)
        {
            this.schoolDbContext = schoolDbContext;
        }

        /* public StudentService()
         {
         }*/

        // Getting Student repo

        public StudentResponseDto AddStudent(StudentRequestDto studentRequestDto)
        {
            // first fetch the Standard and also take standardName input
            // in order to give him a unique roll no

            string standardName=studentRequestDto.StandardName;
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName;
            Standard? standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).Include(x=>x.Students).FirstOrDefault();
            if (standard == null)
                throw new StandardNotFoundException("Standard Not found");

            int NewRollNo = standard.Students.Count + 1;

            Student student = StudentTransformer.StudentRequestDtoToStudent(studentRequestDto);

            student.RollNo = NewRollNo;
            standard.Students.Add(student);// adding the student
            student.Standard = standard;
            //schoolDbContext.Students.Add(student);
            //schoolDbContext.Standards.Update(standard);
            schoolDbContext.SaveChanges();
            StudentResponseDto response= StudentTransformer.StudentToStudentResponseDto(student);
            response.message = "list Size " + standard.Students.Count+ "\n standard name : "+student.Standard.StandardName;
            return response;
        }

        public StudentResponseDto GetStudent(int id)
        {
            if (schoolDbContext.Students == null)
            {
                StudentResponseDto studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;
            }
            Student? student = schoolDbContext.Students.Where(s=>s.Id==id).Include(s=>s.Standard).FirstOrDefault();

            if (student == null)
            {
                StudentResponseDto studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;
            }

            return StudentTransformer.StudentToStudentResponseDto(student);
        }
        public StudentResponseDto UpdateStudent(int id, StudentRequestDto studentRequestDto)
        {
            StudentResponseDto studentResponseDto;
            if (schoolDbContext.Students == null)
            {
                studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;

            }
            Student? student = schoolDbContext.Students.Find(id);

            if (student == null)
            {
                studentResponseDto = new StudentResponseDto();
                studentResponseDto.message = "Student Doesnt Exist";
                return studentResponseDto;
            }
            string oldName = student.Name!;
            student.Name = studentRequestDto.Name!.Equals("") ? student.Name : studentRequestDto.Name;
            student.Age = studentRequestDto.Age == 0 ? student.Age : studentRequestDto.Age;
            student.EmailId = studentRequestDto.EmailId == string.Empty ? student.EmailId : studentRequestDto.EmailId;
            student.MobNo = studentRequestDto.MobNo == string.Empty ? student.MobNo : studentRequestDto.MobNo;
            schoolDbContext.Students.Update(student);
            schoolDbContext.SaveChanges();
            studentResponseDto = StudentTransformer.StudentToStudentResponseDto(student);
            studentResponseDto.message = oldName + " updated to " + student.Name + "Succesfully";
            return studentResponseDto;

        }

        public List<StudentResponseDto> GetAllStudents()
        {
            List<StudentResponseDto> ans = new List<StudentResponseDto>();


            if (schoolDbContext.Students == null)
            {
                return new List<StudentResponseDto>();
            }
            List<Student> students = schoolDbContext.Students.Include(s=>s.Standard).ToList();
            if (students == null)
            {
                return new List<StudentResponseDto>();
            }
            foreach (Student student in students)
            {
                StudentResponseDto studentResponseDto = StudentTransformer.StudentToStudentResponseDto(student);
                ans.Add(studentResponseDto);
            }
            return ans;

        }

        public StudentResponseDto DeleteStudent(int id)
        {
            DbSet<Student> studentRepository = schoolDbContext.Students;
            StudentResponseDto ans;
            if (studentRepository == null)
            {
                ans = new StudentResponseDto();
                ans.message = "Invalid Id";
                return ans;
            }
            Student? student = studentRepository.Find(id);
            if (student == null)
            {
                ans = new StudentResponseDto();
                ans.message = "Invalid Id";
                return ans;
            }
            studentRepository.Remove(student);
            schoolDbContext.SaveChanges();
            ans = new StudentResponseDto();
            ans.message = student.Name + " has been removed Successfully";
            return ans;
        }

        public string AddStudentToAddress(int addressId, int studentId)
        {
            if (schoolDbContext.Students == null)
                throw new StudentNotFoundException("Add student first");
            if (schoolDbContext.StudentAddresses == null)
                throw new StudentAddressNotFound("Add Address first");

            StudentAddress? address = schoolDbContext.StudentAddresses.Find(addressId);
            if (address == null)
                throw new StudentAddressNotFound("Address Not found with given id "+addressId);
            Student? student = schoolDbContext.Students.Find(studentId);
            if (student == null)
                throw new StudentNotFoundException("no student availabe with given id " + studentId);
            address.student = student;
            student.studentAddress = address;
            schoolDbContext.Students.Update(student);
            schoolDbContext.SaveChanges();

            return student.Name + " belongs to " + address.City;
        }
        public string AddStudentToCourse(string standardName, int rollNo, string courseName)
        {
            // find standard
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName='"+standardName+"'";
            Standard? standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).Include(x=>x.Students).FirstOrDefault();
            if (standard == null)
                throw new NoStandardsAvailableException("No such standard available yet");
            // find student from the list having given roll no
            Student student=null;
           foreach(Student student1 in standard.Students.ToList()){
                if (student1.RollNo == rollNo)
                {
                    student = student1;
                    break;
                }
            }
           if(student==null)
            {
                throw new StudentNotFoundException("No student available with the Roll No " + rollNo);
            }
            // find course from course name
            string sqlQuery1= "select * from courses where CourseName ='" + courseName +"'";
            Course? course = schoolDbContext.Courses.Where(x=>x.CourseName==courseName).Include(x=>x.teacher).FirstOrDefault();
            if (course == null)
                throw new CourseNotFound("Course Not Found with Name: " + courseName);
            // find the StudentCourse in the navigation list of student 
            StudentCourse courseByStudent = new StudentCourse() ;

            // Now add Student to this (relationTable)
            courseByStudent.student = student;
            student.StudentCourses.Add(courseByStudent);

            // Now Add Course to this (relation Table)
            courseByStudent.course = course;
            course.StudentCourses.Add(courseByStudent);
            try
            {
                schoolDbContext.Courses.Update(course);
                schoolDbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new StudentAlreadyPresentException(student.Name + "already found with course- " + course.CourseName + "teacher name" + course.teacher.TeacherName);
            }


            return student.Name + " has been Added to " + course.CourseName + " taught by " + course.teacher.TeacherName;
        }

        public List<StudentResponseDto> GetAllStudentFromCity(string city)
        {
            string sqlQuery = "SELECT * FROM studentaddresses s WHERE s.City='" + city+"'" ;
            List<StudentAddress> addresses;
            try
            {
                addresses = schoolDbContext.StudentAddresses.FromSqlRaw(sqlQuery).Include(x=>x.student).ToList();
            }
            catch (Exception ex)
            {
                throw new AddressNotFound("sqlQuery is not working "+sqlQuery);//just for testing purpose
            }

            if (addresses.Count == 0)
                throw new StudentAddressNotFound("No student from city " + city + " available.");
            List<StudentResponseDto> ans = new List<StudentResponseDto>();
            foreach (StudentAddress address in addresses)
            {
                try
                {
                    ans.Add(StudentTransformer.StudentToStudentResponseDto(address.student));

                }
                catch(Exception e)
                {
                    throw new StudentNotFoundException("No Student Assigned to some Address");
                }
            }
            return ans;
        }

        public List<CourseResponseDto> GetAllCoursesByStudent(string emailId)
        {
            string sqlQuery = "SELECT * FROM students s WHERE s.EmailId='" + emailId + "'" ;
            Student? student = schoolDbContext.Students.FromSqlRaw(sqlQuery).Include(x=>x.StudentCourses).ThenInclude(c=>c.course).ThenInclude(c=>c.teacher).FirstOrDefault();
            if (student == null)
            {
                throw new StudentNotFoundException("No student found with email " + emailId);
            }
            List<CourseResponseDto> ans = new List<CourseResponseDto>();
            
            foreach(StudentCourse courseByStudent in student.StudentCourses)
            {
                ans.Add(CourseTransformer.CourseToCourseResponseDto(courseByStudent.course));
            }
            return ans;
        }

        public List<StudentResponseDto> GetStudentWhoIsInStandardAndDoingCourseByTeacher(string standardName, string teacherName)
        {
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName='"+standardName+"'";
            Standard? standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).Include(s => s.Students).ThenInclude(s=>s.StudentCourses).FirstOrDefault();
            if (standard == null)
            {
                throw new StandardNotFoundException("Standar Not found");
            }
            List<Student> students = standard.Students.ToList();

            sqlQuery = "SELECT * FROM teachers t WHERE t.TeacherName='" +teacherName+ "'";
            Teacher? teacher = schoolDbContext.Teachers.FromSqlRaw(sqlQuery).Include(t => t.Courses).ThenInclude(c=>c.StudentCourses).FirstOrDefault();
            if (teacher == null)
            {
                throw new TeacherNotFoundException("Teacher doenst exist with teacher name" + teacherName);
            }

            List<Course> courses = teacher.Courses.ToList();

            List<StudentResponseDto> ans = new();

            foreach(Course course in courses)
            {
                foreach(Student student in students)
                {
                    foreach(StudentCourse courseByStudent in student.StudentCourses.ToList())
                    {
                        if (courseByStudent.course.CourseName!.Equals(course.CourseName))
                        {
                            ans.Add(StudentTransformer.StudentToStudentResponseDto(student));
                        }
                    }
                }
            }
            return ans;
        }
    }
}
