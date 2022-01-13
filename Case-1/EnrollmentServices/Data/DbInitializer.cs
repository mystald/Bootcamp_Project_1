using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Models;

namespace EnrollmentServices.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course
                {
                    Code = "CS001",
                    Name = "C# Beginner",
                    Credit = 2,
                });

                context.Courses.Add(new Course
                {
                    Code = "CS002",
                    Name = "C# Intermediate",
                    Credit = 4,
                });

                context.Courses.Add(new Course
                {
                    Code = "DB001",
                    Name = "Database Design",
                    Credit = 4,
                });

                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                context.Students.Add(new Student
                {
                    FirstName = "Hilman",
                    LastName = "Abdan",
                    BirthDate = System.DateTime.Today,
                });

                context.Students.Add(new Student
                {
                    FirstName = "John",
                    LastName = "Prodman",
                    BirthDate = System.DateTime.Today,
                });

                context.Students.Add(new Student
                {
                    FirstName = "Tyl",
                    LastName = "Regor",
                    BirthDate = System.DateTime.Today,
                });

                context.SaveChanges();
            }


            if (!context.Enrollments.Any())
            {
                context.Enrollments.Add(new Enrollment
                {
                    StudentId = 1,
                    CourseId = 1,
                    EnrollDate = System.DateTime.Today,
                });

                context.Enrollments.Add(new Enrollment
                {
                    StudentId = 1,
                    CourseId = 3,
                    EnrollDate = System.DateTime.Today,
                });

                context.Enrollments.Add(new Enrollment
                {
                    StudentId = 2,
                    CourseId = 2,
                    EnrollDate = System.DateTime.Today,
                });

                context.Enrollments.Add(new Enrollment
                {
                    StudentId = 3,
                    CourseId = 2,
                    EnrollDate = System.DateTime.Today,
                });

                context.Enrollments.Add(new Enrollment
                {
                    StudentId = 3,
                    CourseId = 3,
                    EnrollDate = System.DateTime.Today,
                });

                context.SaveChanges();
            }
        }
    }
}