using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAPI.Dtos;
using GraphQLAPI.Models;

namespace GraphQLAPI.Data
{
    public interface IUser : ICrud<User>
    {
        Task<string> Authentication(string username, string password);
        IQueryable<DtoTwittorGet> GetTwittors(int id);
        IQueryable<Role> GetRoles(int id);
        Task<User> GetByUsername(string username);
    }
}