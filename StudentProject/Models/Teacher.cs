namespace StudentProject.Models
{
    public class Teacher
    {
        public Teacher()
        {
            //this.Courses = new HashSet<Course>();
           // this.Students = new HashSet<Student>();
        }
        // Properties of the class
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Age { get; set; }
        public string EmailId { get; set; }
        public string MobNo { get; set; }
        public string? Subject{ get; set; }
        /* public Teacher(int id, string name, string subject)
         {
             this.Id = id;
             this.Name = name;
             this.Subject = subject;
         }*/
        //public int StudentId { get; set; }  
        //public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        // Navigational Properties
        //public virtual ICollection<Course> Courses { get; set; }
        /*public Nullable<int> StandardId { get; set; }
        public Standard standard { get; set; } = null;*/
        //public virtual ICollection<Student> Students { get; set; };
        
    }
}
