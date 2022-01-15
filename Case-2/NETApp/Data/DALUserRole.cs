using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETApp.Models;

namespace NETApp.Data
{
    public class DALUserRole : IUserRole
    {
        private case2twittorContext _db;

        public DALUserRole(case2twittorContext db)
        {
            _db = db;
        }
        public async Task Delete(UserRole oldObj)
        {
            var userRole = await _db.UserRoles.Where(u => u.Id == oldObj.Id).SingleOrDefaultAsync();

            _db.Remove(userRole);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data deleted from db");
        }

        public async Task Insert(UserRole obj)
        {
            var result = await _db.UserRoles.AddAsync(obj);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data saved to db");
        }

        public Task Update(UserRole newObj)
        {
            throw new NotImplementedException();
        }
    }
}