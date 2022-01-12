using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServices.Dtos
{
    public class DtoUserRoleAssignInput
    {
        public int roleId { get; set; }
    }

    public class DtoUserRoleAssignOutput
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }
}