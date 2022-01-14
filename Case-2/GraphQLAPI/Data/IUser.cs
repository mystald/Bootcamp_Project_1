using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAPI.Models;

namespace GraphQLAPI.Data
{
    public interface IUser : ICrud<User>
    {
        Task<string> Authentication(string username, string password);
        Task<IEnumerable<Twittor>> GetTwittors(int id);
        Task<IEnumerable<Role>> GetRoles(int id);
        Task<User> GetByUsername(string username);
    }
}