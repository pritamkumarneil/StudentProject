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
         public DbSet<StudentAddress> studentAddresses { get; set; }
         /*public DbSet<Course> Courses { get; set; }
         public DbSet<Teacher> Teachers { get; set; }
         public DbSet<Standard> standards { get; set; }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(e => e.RollNo)
                .IsUnique();
            modelBuilder.Entity<Student>()
                .Property(s => s.MobNo)
                .HasMaxLength(10);
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.EmailId)
                .IsUnique();
            // for StudentAddress and Student relation
            modelBuilder.Entity<StudentAddress>()
                .HasOne(sa => sa.student)
                .WithOne(s => s.studentAddress)
                .HasForeignKey<StudentAddress>(sa => sa.StudentId);
            // for teacher Entity class
           /* modelBuilder.Entity<Teacher>()
                .Property(t => t.MobNo)
                .HasMaxLength(10);
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.MobNo)
                .IsUnique();
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.EmailId)
                .IsUnique();*/
            // for 
           /*modelBuilder.Entity<Student>()
                .HasOne()*/
        }
    }
}
