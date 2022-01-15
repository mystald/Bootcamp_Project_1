using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETApp.Models;

namespace NETApp.Data
{
    public class DALTwittor : ITwittor
    {
        private case2twittorContext _db;

        public DALTwittor(case2twittorContext db)
        {
            _db = db;
        }
        public async Task Delete(Twittor oldObj)
        {
            var twittor = await _db.Twittors.Where(u => u.Id == oldObj.Id).SingleOrDefaultAsync();

            _db.Remove(twittor);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data deleted from db");
        }

        public async Task Insert(Twittor obj)
        {
            var result = await _db.Twittors.AddAsync(obj);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data saved to db");
        }

        public async Task Update(Twittor newObj)
        {
            var twittor = await _db.Twittors.Where(u => u.Id == newObj.Id).SingleOrDefaultAsync();

            twittor.Content = newObj.Content;

            await _db.SaveChangesAsync();

            Console.WriteLine("Data saved to db");
        }
    }
}