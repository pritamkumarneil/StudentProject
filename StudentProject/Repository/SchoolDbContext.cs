using Microsoft.EntityFrameworkCore;
using StudentProject.Models;
using System.Xml;

namespace StudentProject.Repository
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Student>()
                .HasIndex(e => e.RollNo)
                .IsUnique();*/
            modelBuilder.Entity<Student>()
                .Property(s => s.MobNo)
                .HasMaxLength(10);
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.EmailId)
                .IsUnique();
            // Defining properties and all for Standard
            modelBuilder.Entity<Standard>()
                .HasIndex(s=>s.StandardName)
                .IsUnique();
            // relation between student and standard
            modelBuilder.Entity<Student>()
                .HasOne<Standard>(s =>s.Standard)
                .WithMany(s=>s.Students)
                .HasForeignKey(s=>s.StandardId);
            // for StudentAddress and Student relation
            modelBuilder.Entity<StudentAddress>()
                .HasOne(sa => sa.student)
                .WithOne(s => s.studentAddress)
                .HasForeignKey<StudentAddress>(sa => sa.StudentId);

            // for teacher Entity class
            modelBuilder.Entity<Teacher>()
                .Property(t => t.MobNo)
                .HasMaxLength(10);
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.MobNo)
                .IsUnique();
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.EmailId)
                .IsUnique();
            // for course Entity
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.CourseName)
                .IsUnique();
            // Defining the relation between Teacher and standard
            modelBuilder.Entity<Teacher>()
                .HasOne<Standard>(t => t.standard)
                .WithMany(st => st.Teachers)
                .HasForeignKey(t => t.StandardId);
            // Defining the relation between Teacher and course
            modelBuilder.Entity<Course>()
                .HasOne<Teacher>(c => c.teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId);

            // Defining the Relation between Student and Course(ManyToMany)
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });// here we are defining two key
            // student(one) To StudentCourses(Many)
            modelBuilder.Entity<StudentCourse>()
                .HasOne<Student>(sc => sc.student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);
            // Course(One) To StudentCourse(Many)
            modelBuilder.Entity<StudentCourse>()
                .HasOne<Course>(sc => sc.course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
