using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAPI.Models;

namespace GraphQLAPI.Data
{
    public interface IUserRole : ICrud<UserRole>
    {
        Task<UserRole> GetByUserAndRoleId(int userId, int roleId);
        Task<UserRole> Delete(int userId, int roleId);
    }
}