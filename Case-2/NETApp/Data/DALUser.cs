using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETApp.Models;

namespace NETApp.Data
{
    public class DALUser : IUser
    {
        private case2twittorContext _db;

        public DALUser(case2twittorContext db)
        {
            _db = db;
        }
        public async Task Delete(User oldObj)
        {
            var user = await _db.Users.Where(u => u.Id == oldObj.Id).SingleOrDefaultAsync();

            _db.Remove(user);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data deleted from db");
        }

        public async Task Insert(User obj)
        {
            var result = await _db.Users.AddAsync(obj);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data saved to db");
        }

        public async Task Update(User newObj)
        {
            var user = await _db.Users.Where(u => u.Id == newObj.Id).SingleOrDefaultAsync();

            user.Username = newObj.Username;
            user.Password = newObj.Password;
            user.Bio = newObj.Bio;
            user.IsLocked = newObj.IsLocked;

            await _db.SaveChangesAsync();

            Console.WriteLine("Data saved to db");
        }
    }
}