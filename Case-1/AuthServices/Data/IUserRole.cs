using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Models;

namespace AuthServices.Data
{
    public interface IUserRole : ICrud<UserRole>
    {
        Task<UserRole> GetByUserIdRoleId(int userId, int roleId);
    }
}