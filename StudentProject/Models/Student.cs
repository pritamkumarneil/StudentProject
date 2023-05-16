using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentProject.Models
{
    public class Student
    {
        public Student()
        {
           /* this.Courses = new HashSet<Course>();
            this.studentAddress = null;
            this.standard = null;*/
        }
        // Properties of the class
        public int Id { get; set; }
        public string? Name { get; set; }
        public string EmailId { get; set; }
        public string MobNo { get; set; }
        public int Age { get; set; }
        public int RollNo { get; set; }
        public string? Branch { get; set; }
        public DateTime StudentDateOfBirth { get; set; }
        //public Nullable<int> TeacherId { get; set; }//(parent for this entity(FK))

        //public ICollection<Teacher> Teachers { get; set; } = new HashSet<Teacher>();

        // Navigational properties


        /*public Nullable<int> CourseId { get; set; }// parent for this entity(FK)// many to many
        [JsonIgnore]
        public ICollection<Course> Courses { get; set; }

        *//*       public Nullable<int> StudentAddressId { set; get; } = default(Nullable<int>);*//*
        [JsonIgnore]
        public StudentAddress studentAddress { get; set; } 
        [ForeignKey("StudentId")]
        public Nullable<int> StandardId { get; set; }
        [JsonIgnore]
        public Standard standard { get; set; } = null;*/
        

    }
}
