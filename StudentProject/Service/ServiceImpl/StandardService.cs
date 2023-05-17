using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Exceptions;
using StudentProject.Models;
using StudentProject.Repository;
using StudentProject.Transformer;

namespace StudentProject.Service.ServiceImpl
{
    public class StandardService  : IStandardService
    {
        private SchoolDbContext schoolDbContext;
        public StandardService(SchoolDbContext schoolDb)
        {
           this.schoolDbContext = schoolDb;
        }

        public StandardResponseDto AddStandard(StandardRequestDto standardRequestDto)
        {
            Standard standard = StandardTransformer.StandardRequestDtoToStandard(standardRequestDto);
            schoolDbContext.Standards.Add(standard);
            StandardResponseDto response = StandardTransformer.StandardToStandardResponseDto(standard);
            response.Message="Added successfully!";
            return response;
        }

        public string AddStudentToStandard(string standardName, int studentId)
        {
            if (schoolDbContext.Standards == null)
                throw new NoStandardsAvailableException("No standard Added yet");
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName + ";";
            Standard standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).First();

            if (schoolDbContext.Students == null)
                throw new StudentNotFoundException("Please add Student First");
            Student student = schoolDbContext.Students.Find(studentId);
            if (student == null)
                throw new StudentNotFoundException("Student Not found");
            // if both found then add eachother to eachother
            student.Standard = standard;
            standard.Students.Add(student);

            schoolDbContext.Standards.Update(standard);
            schoolDbContext.SaveChanges();
            return student.Name + " added to " + standardName;

        }

        public string AddTeacherToStandard(string mobNo, string standardName)
        {
            if (schoolDbContext.Teachers == null)
                throw new TeacherNotFoundException("No teacher registered yet");
            string sqlQuery = "SELECT * FROM teachers t WHERE t.MobNo="+mobNo+";";
            Teacher teacher = schoolDbContext.Teachers.FromSqlRaw(sqlQuery).First();
            if (teacher == null)
                throw new TeacherNotFoundException("Teacher Doesn't Exist with mobile " + mobNo);
            // now find the standard
            if (schoolDbContext.Standards == null)
                throw new NoStandardsAvailableException("No standard Added yet");
            sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName + ";";
            Standard standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).First();

            // after finding both teacher and standard add eachother into eachOther
            standard.Teachers.Add(teacher);
            teacher.standard = standard;

            schoolDbContext.Standards.Update(standard);
            schoolDbContext.SaveChanges();
            return teacher.TeacherName + " added Successfully to " + standardName;
        }

        public List<StudentResponseDto> GetAllStudentsFromStandard(string standardName)
        {
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName="+standardName+";";
            Standard standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).First();
            if (standard == null)
                throw new NoStandardsAvailableException("No standard availabe with name " + standardName);
            List<Student> students = standard.Students.ToList();

            List<StudentResponseDto> ans = new List<StudentResponseDto>();
            foreach(Student student in students)
            {
                ans.Add(StudentTransformer.StudentToStudentResponseDto(student));
            }
            return ans;
        }

        public List<TeacherResponseDto> GetAllTeachersFromStandard(string standardName)
        {
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName + ";";
            Standard standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).First();
            if (standard == null)
                throw new NoStandardsAvailableException("No standard availabe with name " + standardName);
            List<Teacher> teachers = standard.Teachers.ToList();
            if (teachers == null)
                throw new TeacherNotFoundException("No Teachers Found in " + standardName);

            List<TeacherResponseDto> ans = new List<TeacherResponseDto>();
            foreach(Teacher teacher in teachers)
            {
                ans.Add(TeacherTransformer.TeacherToTeacherResponseDto(teacher));
            }
            return ans;
        }

        public List<StandardResponseDto> GetStandards()
        {
            if (schoolDbContext.Standards == null)
                throw new NoStandardsAvailableException("No standard are added yet");
            List<Standard> standards = schoolDbContext.Standards.ToList();

            List<StandardResponseDto> ans = new List<StandardResponseDto>();

            foreach(Standard standard in standards)
            {
                ans.Add(StandardTransformer.StandardToStandardResponseDto(standard));
            }
            return ans;

        }
    }
}
