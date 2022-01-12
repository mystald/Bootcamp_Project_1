using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Exceptions;
using AuthServices.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthServices.Data
{
    public class DALRole : IRole
    {
        private ApplicationDbContext _db;

        public DALRole(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<Role> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            var results = await _db.Roles.ToListAsync();

            if (!results.Any()) throw new DataNotFoundException("Roles not found");

            return results;
        }

        public async Task<Role> GetById(int id)
        {
            var result = await _db.Roles.Where(
                role => role.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("UserRole not found");

            return result;
        }

        public Task<Role> Insert(Role obj)
        {
            throw new NotImplementedException();
        }

        public Task<Role> Update(int id, Role obj)
        {
            throw new NotImplementedException();
        }
    }
}