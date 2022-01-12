using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthServices.Data
{
    public interface IUser : ICrud<User>
    {
        Task<string> Authenticate(string username, string password);
        Task<IEnumerable<Role>> GetRoles(int userId);
        Task<User> GetByUsername(string username);
    }
}