using Microsoft.EntityFrameworkCore;
using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Exceptions;
using StudentProject.Models;
using StudentProject.Repository;
using StudentProject.Transformer;

namespace StudentProject.Service.ServiceImpl
{
    public class TeacherService : ITeacherService
    {
        private SchoolDbContext schoolDbContext;
        public TeacherService(SchoolDbContext schoolDb) 
        {
            this.schoolDbContext = schoolDb;
        }
        public TeacherResponseDto AddTeacher(TeacherRequestDto request)
        {
            //first get the standard id from request Dto
            int standardId = request.StandardId;
            Standard standard=schoolDbContext.Standards.Find(standardId);
            if (standard == null)
            {
                throw new StandardNotFoundException("Please Enter Valid StandardId");
            }
            Teacher teacher = TeacherTransformer.TeacherRequestDtoToTeacher(request);
            teacher.standard = standard;
            standard.Teachers.Add(teacher);
            schoolDbContext.Teachers.Add(teacher);
            schoolDbContext.SaveChanges();

            return TeacherTransformer.TeacherToTeacherResponseDto(teacher);

        }

        public string AddTeacherToStandard(string StandardName,int teacherId)
        {
            string sqlQuery = "SELECT * FROM standards s WHERE s.StandardName="+StandardName;
            Standard standard = schoolDbContext.Standards.FromSqlRaw(sqlQuery).First();
            if (standard == null)
                throw new StandardNotFoundException("No standard available with name" + StandardName);
            Teacher teacher = schoolDbContext.Teachers.Find(teacherId);
            if (teacher == null)
                throw new TeacherNotFoundException("Teacher Doesn't exist with given" + teacherId);
            teacher.standard = standard;
            standard.Teachers.Add(teacher);
            // simply doing this will also work
            //schoolDbContext.SaveChanges();
            // but to be in safer side we will update the standard
            schoolDbContext.Standards.Update(standard);
            schoolDbContext.SaveChanges();


            return teacher.TeacherName + " Added Successfully to " + StandardName;
        }

        List<TeacherResponseDto> ITeacherService.GetAllTeacher()
        {
            List<Teacher> teachers = schoolDbContext.Teachers.ToList();
            List<TeacherResponseDto> ans = new List<TeacherResponseDto>();
            foreach (Teacher teacher in teachers) ans.Add(TeacherTransformer.TeacherToTeacherResponseDto(teacher));
            return ans;
        }

        List<TeacherResponseDto> ITeacherService.GetAllTeacherOfGivenStandard(int standardId)
        {
            if (schoolDbContext.Standards == null)
                throw new NoStandardsAvailableException("No standards Found Yet");
            Standard standard = schoolDbContext.Standards.Find(standardId)!;
            if (standard == null) 
                throw new StandardNotFoundException("please enter valid standardId");
            List<Teacher> teachers = schoolDbContext.Teachers.ToList();
            List<TeacherResponseDto> ans = new List<TeacherResponseDto>();
            foreach (Teacher teacher in teachers)
            {
                if(standard.Equals(teacher.standard))
                ans.Add(TeacherTransformer.TeacherToTeacherResponseDto(teacher));
            }
            return ans;

        }

        TeacherResponseDto ITeacherService.GetTeacherById(int id)
        {
            Teacher teacher = schoolDbContext.Teachers.Find(id);
            if (teacher == null)
                throw new TeacherNotFoundException("Please Enter Valid TeacherId");
            return TeacherTransformer.TeacherToTeacherResponseDto(teacher);
        }
    }
}
