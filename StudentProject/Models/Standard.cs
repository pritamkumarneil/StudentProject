namespace StudentProject.Models
{
    public class Standard
    {
        public Standard()
        {
            this.Teachers = new HashSet<Teacher>();
            this.Students=new HashSet<Student>();
        }
        // Properties of the class
        public int StandardId { get; set; }
        public string StandardName { get; set; }
        public string StandardDescription { get; set; }

        //Navigational Properties
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
