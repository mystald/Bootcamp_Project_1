using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Exceptions;
using EnrollmentServices.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentServices.Data
{
    public class DALCourse : ICourse
    {
        private ApplicationDbContext _db;

        public DALCourse(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Course> Delete(int id)
        {
            try
            {
                var oldCourse = await GetById(id);

                _db.Remove(oldCourse);

                await _db.SaveChangesAsync();

                return oldCourse;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _db.Courses.ToListAsync();

            if (!results.Any()) throw new DataNotFoundException();

            return results;
        }

        public async Task<Course> GetById(int courseId)
        {
            var result = await _db.Courses.Where(
                course => course.Id == courseId
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException();

            return result;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                var result = await _db.Courses.AddAsync(obj);

                await _db.SaveChangesAsync();

                return result.Entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Course> Update(int id, Course obj)
        {
            try
            {
                var oldCourse = await GetById(id);

                if (obj.Code != null) oldCourse.Code = obj.Code;
                if (obj.Name != null) oldCourse.Name = obj.Name;
                if (obj.Credit != null) oldCourse.Credit = obj.Credit;

                await _db.SaveChangesAsync();

                return oldCourse;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}