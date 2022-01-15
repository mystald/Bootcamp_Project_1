using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETApp.Models;

namespace NETApp.Data
{
    public class DALComment : IComment
    {
        private case2twittorContext _db;

        public DALComment(case2twittorContext db)
        {
            _db = db;
        }
        public async Task Delete(Comment oldObj)
        {
            var comment = await _db.Comments.Where(u => u.Id == oldObj.Id).SingleOrDefaultAsync();

            _db.Remove(comment);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data deleted from db");
        }

        public async Task Insert(Comment obj)
        {
            var result = await _db.Comments.AddAsync(obj);

            await _db.SaveChangesAsync();

            Console.WriteLine("Data saved to db");
        }

        public async Task Update(Comment newObj)
        {
            var comment = await _db.Comments.Where(u => u.Id == newObj.Id).SingleOrDefaultAsync();

            comment.Content = newObj.Content;

            await _db.SaveChangesAsync();

            Console.WriteLine("Data saved to db");
        }
    }
}