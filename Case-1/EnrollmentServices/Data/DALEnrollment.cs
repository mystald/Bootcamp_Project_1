using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Exceptions;
using EnrollmentServices.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentServices.Data
{
    public class DALEnrollment : IEnrollment
    {
        private ApplicationDbContext _db;

        public DALEnrollment(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<Enrollment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _db.Enrollments.ToListAsync();

            if (!results.Any()) throw new DataNotFoundException("Enrollment not found");

            return results;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var result = await _db.Enrollments.Where(
                enroll => enroll.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("Enrollment not found");

            return result;
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                var result = await _db.Enrollments.AddAsync(obj);

                await _db.SaveChangesAsync();

                return result.Entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Enrollment> Update(int id, Enrollment obj)
        {
            try
            {
                var oldEnroll = await GetById(id);

                oldEnroll.StudentId = obj.StudentId;
                oldEnroll.CourseId = obj.CourseId;
                oldEnroll.EnrollDate = obj.EnrollDate;

                await _db.SaveChangesAsync();

                return oldEnroll;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}