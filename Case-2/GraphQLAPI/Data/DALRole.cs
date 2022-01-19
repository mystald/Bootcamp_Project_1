using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAPI.Exceptions;
using GraphQLAPI.Models;

namespace GraphQLAPI.Data
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

        public IQueryable<Role> GetAll()
        {
            var results = _db.Roles.AsQueryable();
            if (!results.Any()) throw new DataNotFoundException("Roles not found");

            return results;
        }

        public Task<Role> GetById(int id)
        {
            throw new NotImplementedException();
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