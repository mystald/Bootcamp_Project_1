using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAPI.Models;

namespace GraphQLAPI.Data
{
    public interface ITwittor : ICrud<Twittor>
    {
        Task<IQueryable<Comment>> GetCommentsByTwittorId(int twittorId);
    }
}