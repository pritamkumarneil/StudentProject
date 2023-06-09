﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentProject.Repository;

#nullable disable

namespace StudentProject.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20230518105312_final")]
    partial class final
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StudentProject.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("CourseName")
                        .IsUnique();

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentProject.Models.Standard", b =>
                {
                    b.Property<int>("StandardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("StandardDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StandardName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("StandardId");

                    b.HasIndex("StandardName")
                        .IsUnique();

                    b.ToTable("Standards");
                });

            modelBuilder.Entity("StudentProject.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Branch")
                        .HasColumnType("longtext");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MobNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("RollNo")
                        .HasColumnType("int");

                    b.Property<int>("StandardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StudentDateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("EmailId")
                        .IsUnique();

                    b.HasIndex("MobNo")
                        .IsUnique();

                    b.HasIndex("StandardId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentProject.Models.StudentAddress", b =>
                {
                    b.Property<int>("StudentAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("StudentAddressId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentAddresses");
                });

            modelBuilder.Entity("StudentProject.Models.StudentCourse", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("StudentProject.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MobNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<int>("StandardId")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("longtext");

                    b.Property<string>("TeacherName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TeacherId");

                    b.HasIndex("EmailId")
                        .IsUnique();

                    b.HasIndex("MobNo")
                        .IsUnique();

                    b.HasIndex("StandardId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("StudentProject.Models.Course", b =>
                {
                    b.HasOne("StudentProject.Models.Teacher", "teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("teacher");
                });

            modelBuilder.Entity("StudentProject.Models.Student", b =>
                {
                    b.HasOne("StudentProject.Models.Standard", "Standard")
                        .WithMany("Students")
                        .HasForeignKey("StandardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("StudentProject.Models.StudentAddress", b =>
                {
                    b.HasOne("StudentProject.Models.Student", "student")
                        .WithOne("studentAddress")
                        .HasForeignKey("StudentProject.Models.StudentAddress", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");
                });

            modelBuilder.Entity("StudentProject.Models.StudentCourse", b =>
                {
                    b.HasOne("StudentProject.Models.Course", "course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentProject.Models.Student", "student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("student");
                });

            modelBuilder.Entity("StudentProject.Models.Teacher", b =>
                {
                    b.HasOne("StudentProject.Models.Standard", "standard")
                        .WithMany("Teachers")
                        .HasForeignKey("StandardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("standard");
                });

            modelBuilder.Entity("StudentProject.Models.Course", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("StudentProject.Models.Standard", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("StudentProject.Models.Student", b =>
                {
                    b.Navigation("StudentCourses");

                    b.Navigation("studentAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("StudentProject.Models.Teacher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
