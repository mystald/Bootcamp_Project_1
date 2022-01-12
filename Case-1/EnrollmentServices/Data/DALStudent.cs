using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Dtos;
using EnrollmentServices.Exceptions;
using EnrollmentServices.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentServices.Data
{
    public class DALStudent : IStudent
    {
        private ApplicationDbContext _db;

        public DALStudent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Student> Delete(int id)
        {
            try
            {
                var oldStudent = await GetById(id);

                _db.Remove(oldStudent);

                await _db.SaveChangesAsync();

                return oldStudent;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await _db.Students.ToListAsync();
            if (!results.Any()) throw new DataNotFoundException("Student not found");

            return results;
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _db.Students.Where(
                student => student.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("Student not found");

            return result;
        }

        public async Task<IEnumerable<DtoStudentEnrollsGet>> GetEnrollments(int StudentId)
        {
            var results = await (
                from enroll in _db.Enrollments
                join course in _db.Courses on enroll.CourseId equals course.Id
                where enroll.StudentId == StudentId
                select new { enrolls = enroll, courses = course }
            ).ToListAsync();

            if (!results.Any()) throw new DataNotFoundException();

            List<DtoStudentEnrollsGet> enrollments = new List<DtoStudentEnrollsGet>();

            foreach (var result in results)
            {
                enrollments.Add(new DtoStudentEnrollsGet
                {
                    Code = result.courses.Code,
                    CourseName = result.courses.Name,
                    Credit = result.courses.Credit,
                    EnrollDate = result.enrolls.EnrollDate,
                    Grade = result.enrolls.Grade.ToString(),
                });
            }

            return enrollments;
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                var result = await _db.Students.AddAsync(obj);

                await _db.SaveChangesAsync();

                return result.Entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Student> Update(int id, Student obj)
        {
            try
            {
                var oldStudent = await GetById(id);

                if (obj.FirstName != null) oldStudent.FirstName = obj.FirstName;
                if (obj.LastName != null) oldStudent.LastName = obj.LastName;
                if (obj.BirthDate != null) oldStudent.BirthDate = obj.BirthDate;

                await _db.SaveChangesAsync();

                return oldStudent;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}