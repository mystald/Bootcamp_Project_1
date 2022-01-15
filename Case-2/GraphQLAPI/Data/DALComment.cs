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
    public class DALComment : IComment
    {
        private ApplicationDbContext _db;
        private Producer _kafka;
        private IUser _user;
        private ITwittor _twittor;

        public DALComment(ApplicationDbContext db, Producer kafka, IUser user, ITwittor twittor)
        {
            _db = db;
            _kafka = kafka;
            _user = user;
            _twittor = twittor;
        }

        public async Task<Comment> Delete(int id)
        {
            try
            {
                var oldComment = await GetById(id);

                _kafka.SendMessage("comment", "delete", JsonSerializer.Serialize(oldComment));

                return oldComment;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public IQueryable<Comment> GetAll()
        {
            var results = _db.Comments.AsQueryable();
            if (!results.Any()) throw new DataNotFoundException("Comment not found");

            return results;
        }

        public async Task<Comment> GetById(int id)
        {
            var result = await _db.Comments.Where(
                comment => comment.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("Comment not found");

            return result;
        }

        public async Task<Comment> Insert(Comment obj)
        {
            try
            {
                await _user.GetById(obj.UserId);

                await _twittor.GetById(obj.TwittorId);

                _kafka.SendMessage("comment", "insert", JsonSerializer.Serialize(obj));

                return obj;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Comment> Update(int id, Comment obj)
        {
            try
            {
                var oldComment = await GetById(id);

                if (obj.Content != null) oldComment.Content = obj.Content;

                _kafka.SendMessage("comment", "update", JsonSerializer.Serialize(oldComment));

                return oldComment;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}