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
        private readonly SchoolDbContext schoolDbContext;
        public StandardService(SchoolDbContext schoolDb)
        {
           this.schoolDbContext = schoolDb;
        }

        public StandardResponseDto AddStandard(StandardRequestDto standardRequestDto)
        {
            Standard standard = StandardTransformer.StandardRequestDtoToStandard(standardRequestDto);
            Console.WriteLine(standard.ToString());
            schoolDbContext.Standards.Add(standard);
            StandardResponseDto response = StandardTransformer.StandardToStandardResponseDto(standard);
            response.Message="Added successfully!";
            schoolDbContext.SaveChanges();
            return response;
        }

        public string AddStudentToStandard(string standardName, int studentId)
        {
            if (schoolDbContext.Standards == null)
                throw new NoStandardsAvailableException("No standard Added yet");
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName='" + standardName+"'";
            Standard? standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).FirstOrDefault();
            if (standard == null)
            {
                throw new StandardNotFoundException("standard wiht name " + standardName + " Not found");
            }

            if (schoolDbContext.Students == null)
                throw new StudentNotFoundException("Please add Student First");
            Student? student = schoolDbContext.Students.Find(studentId);
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
            string sqlQuery = "SELECT * FROM teachers t WHERE t.MobNo=" + mobNo;
            Teacher teacher = schoolDbContext.Teachers.FromSqlRaw(sqlQuery).First();
            if (teacher == null)
                throw new TeacherNotFoundException("Teacher Doesn't Exist with mobile " + mobNo);
            // now find the standard
            if (schoolDbContext.Standards == null)
                throw new NoStandardsAvailableException("No standard Added yet");
            sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName;
            Standard? standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).FirstOrDefault();
            if (standard == null)
            {
                throw new StandardNotFoundException("standard Name " + standardName + " is Not yet Added");
            }

            // after finding both teacher and standard add eachother into eachOther
            standard.Teachers.Add(teacher);
            teacher.standard = standard;

            schoolDbContext.Standards.Update(standard);
            schoolDbContext.SaveChanges();
            return teacher.TeacherName + " added Successfully to " + standardName;
        }

        public List<StudentResponseDto> GetAllStudentsFromStandard(string standardName)
        { 
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName;
            Standard? standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).Include(x => x.Students).FirstOrDefault();
            if (standard == null)
                throw new NoStandardsAvailableException("No standard availabe with name " + standardName);
            List<Student> students = standard.Students.ToList();
            if (students.Count == 0)
                throw new StudentNotFoundException("No Students are There yet in this Standard");

            List<StudentResponseDto> ans = new List<StudentResponseDto>();
            foreach(Student student in students)
            {
                ans.Add(StudentTransformer.StudentToStudentResponseDto(student));
            }
            return ans;
        }

        public List<TeacherResponseDto> GetAllTeachersFromStandard(string standardName)
        {
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName=" + standardName;
            Standard? standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).Include(x=>x.Teachers).FirstOrDefault();
            if (standard == null)
                throw new NoStandardsAvailableException("No standard availabe with name " + standardName);
            
            List<Teacher> teachers = standard.Teachers.ToList() ?? throw new TeacherNotFoundException("No Teachers Found in " + standardName);
            
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

        List<StudentResponseDto> IStandardService.GetAllStudentsFromCityInStandard(string city, string standardName)
        {
            // we can perform join operation also using LINQ or SQL query but going with oldScholl method
            string sqlQuery = "SELECT * FROM studentaddresses sa WHERE sa.City='" + city + "'";
            List<StudentAddress> addresses = schoolDbContext.StudentAddresses.FromSqlRaw(sqlQuery).Include(x => x.student).ToList();


            Standard? standard = schoolDbContext.Standards.Where(x => x.StandardName.Equals(standardName)).Include(s => s.Students).FirstOrDefault();

            if (standard == null)
            {
                throw new StandardNotFoundException("standar with name " + standardName + " Not found");
            }

            List<StudentResponseDto> students = new();
            foreach(StudentAddress address in addresses)
            {
                foreach(Student student in standard.Students.ToList())
                {
                    if (student.Equals(address.student))
                    {
                        students.Add(StudentTransformer.StudentToStudentResponseDto(student));
                    }
                }
            }
            return students;
        }
    }
}
