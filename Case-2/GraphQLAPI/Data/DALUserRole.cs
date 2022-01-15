using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQLAPI.Exceptions;
using GraphQLAPI.Kafka;
using GraphQLAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI.Data
{
    public class DALUserRole : IUserRole
    {
        private ApplicationDbContext _db;
        private Producer _kafka;

        public DALUserRole(ApplicationDbContext db, Producer kafka)
        {
            _db = db;
            _kafka = kafka;
        }

        public async Task<UserRole> Delete(int userId, int roleId)
        {
            try
            {
                var oldUserRole = await GetByUserAndRoleId(userId, roleId);

                _kafka.SendMessage("userrole", "delete", JsonSerializer.Serialize(oldUserRole));
                _kafka.SendMessage("loggings", "userrole-delete", JsonSerializer.Serialize(oldUserRole));

                return oldUserRole;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<UserRole> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRole> GetByUserAndRoleId(int userId, int roleId)
        {
            var result = await _db.UserRoles.Where(
                userrole => userrole.UserId == userId && userrole.RoleId == roleId
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("UserRole not found");

            return result;
        }

        public async Task<UserRole> Insert(UserRole obj)
        {
            try
            {
                try
                {
                    var userRoleExist = await GetByUserAndRoleId(obj.UserId, obj.RoleId);

                    if (userRoleExist != null) throw new System.Exception("Role has already been assigned");
                }
                catch (DataNotFoundException)
                {
                    // Do Nothin'
                }

                _kafka.SendMessage("userrole", "insert", JsonSerializer.Serialize(obj));
                _kafka.SendMessage("loggings", "userrole-insert", JsonSerializer.Serialize(obj));

                return obj;
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