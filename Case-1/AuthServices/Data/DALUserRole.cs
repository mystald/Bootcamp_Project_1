using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Exceptions;
using AuthServices.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthServices.Data
{
    public class DALUserRole : IUserRole
    {
        private ApplicationDbContext _db;

        public DALUserRole(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<UserRole> Delete(int id)
        {
            try
            {
                var oldUserRole = await GetById(id);

                _db.Remove(oldUserRole);

                await _db.SaveChangesAsync();

                return oldUserRole;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<UserRole>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserRole> GetById(int id)
        {
            var result = await _db.UserRoles.Where(
                userRole => userRole.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("UserRole not found");

            return result;
        }

        public async Task<UserRole> GetByUserIdRoleId(int userId, int roleId)
        {
            var result = await _db.UserRoles.Where(
                userRole => userRole.UserId == userId && userRole.RoleId == roleId
            ).FirstOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("UserRole not found");

            return result;
        }

        public async Task<UserRole> Insert(UserRole obj)
        {
            try
            {
                var result = await _db.UserRoles.AddAsync(obj);

                await _db.SaveChangesAsync();

                return result.Entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<UserRole> Update(int id, UserRole obj)
        {
            throw new NotImplementedException();
        }
    }
}