using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentProject.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }
        // Properties of the class
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string Location { get; set; }
        //public System.Data.Entity.Spatial.DbGeography Location { get; set; }

        // Navigational Properties
        [ForeignKey("Teacher")]
        public Nullable<int> TeacherId { get; set; }// teacher is parent to this class so (FK)
        [JsonIgnore]
        public Teacher Teacher { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }


    }
}
