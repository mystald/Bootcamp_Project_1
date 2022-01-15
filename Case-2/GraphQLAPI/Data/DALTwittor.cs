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
    public class DALTwittor : ITwittor
    {
        private ApplicationDbContext _db;
        private Producer _kafka;

        public DALTwittor(ApplicationDbContext db, Producer kafka)
        {
            _db = db;
            _kafka = kafka;
        }

        public async Task<Twittor> Delete(int id)
        {
            try
            {
                var oldTwittor = await GetById(id);

                _kafka.SendMessage("twittor", "delete", JsonSerializer.Serialize(oldTwittor));
                _kafka.SendMessage("loggings", "twittor-delete", JsonSerializer.Serialize(oldTwittor));

                return oldTwittor;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public IQueryable<Twittor> GetAll()
        {
            var results = _db.Twittors.AsQueryable();
            if (!results.Any()) throw new DataNotFoundException("Twittor not found");

            return results;
        }

        public async Task<Twittor> GetById(int id)
        {
            var result = await _db.Twittors.Where(
                twittor => twittor.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("Twittor not found");

            return result;
        }

        public async Task<IQueryable<Comment>> GetCommentsByTwittorId(int twittorId)
        {
            await GetById(twittorId);

            var results = (
                from comment in _db.Comments
                where comment.TwittorId == twittorId
                select comment
            ).AsQueryable();

            if (!results.Any()) throw new DataNotFoundException("Comments not found");

            return results;
        }

        public async Task<Twittor> Insert(Twittor obj)
        {
            try
            {
                _kafka.SendMessage("twittor", "insert", JsonSerializer.Serialize(obj));
                _kafka.SendMessage("loggings", "twittor-insert", JsonSerializer.Serialize(obj));

                return obj;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Twittor> Update(int id, Twittor obj)
        {
            try
            {
                var oldTwittor = await GetById(id);

                if (obj.Content != null) oldTwittor.Content = obj.Content;

                _kafka.SendMessage("twittor", "update", JsonSerializer.Serialize(oldTwittor));
                _kafka.SendMessage("loggings", "twittor-update", JsonSerializer.Serialize(oldTwittor));

                return oldTwittor;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}