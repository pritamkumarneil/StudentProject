using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;
using StudentProject.Repository;

namespace StudentProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolDbContext schoolDbContext;

        public SchoolController(SchoolDbContext schoolDbContext)
        {
            this.schoolDbContext = schoolDbContext;
        }

        [HttpGet("getAllStudent")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudent()
        {
            if (schoolDbContext.Students == null)
            {
                return NotFound();
            }
            return await schoolDbContext.Students.ToListAsync();
        }
        /*[HttpGet("getTeachersByStudentId/{id}")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachersByStudentId(int studentId)
        {
            Student student = await schoolDbContext.Students.FindAsync(studentId);
            if (student == null) return BadRequest();
            return student.Teachers.ToList();
        }*/
       /* [HttpPost("addStudent")]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            schoolDbContext.Students.Add(student);
            await schoolDbContext.SaveChangesAsync();
            return Ok(student);
        }
        [HttpPost("add-course")]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            schoolDbContext.Courses.Add(course);
            await schoolDbContext.SaveChangesAsync();
            return Ok(course);
        }
        [HttpPost("addTeacher")]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            schoolDbContext.Teachers.Add(teacher);
            await schoolDbContext.SaveChangesAsync();
            return Ok(teacher);
        }*/
       /* [HttpPut("joinStudentTeacher/{studentId}/{teacherId}")]
        public async Task<ActionResult<string>> PutStudentTeacherJoin(int studentId,int teacherId)
        {
            Student student = await schoolDbContext.Students.FindAsync(studentId);
            if(student==null)
            {
                return BadRequest();
            }

            Teacher teacher = await schoolDbContext.Teachers.FindAsync(teacherId);
            if (teacher == null)
            {
                return BadRequest();
            }
            teacher.Students.Add(student);// here Add is adding student into the list of student
            student.Teachers.Add(teacher);// teacher is getting added to the list of the teacher inside the student
            schoolDbContext.Teachers.Add(teacher);
            schoolDbContext.SaveChanges();

            return Ok("Successfully Added");
        }*/
    }
}
