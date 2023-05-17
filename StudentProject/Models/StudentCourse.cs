namespace StudentProject.Models
{
    public class StudentCourse
    {

        // this table is just for making many to many relationship between
        // Student and the course- by using this table
        // this table will have the ManyToOne (this and Student) reltion with Student 
        // same will happen with course entity also
        // these are navigationalProperties Only 
        public int StudentId { get; set; }
        public Student student { get; set; }// student is parent to this entity

        public int CourseId { get; set; }
        public Course course { get; set; }// course is parent to this entity
    }
}
