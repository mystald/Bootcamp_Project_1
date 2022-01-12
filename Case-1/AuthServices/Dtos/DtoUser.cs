using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AuthServices.Dtos
{
    public class DtoUserAuthInput
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class DtoUserAuthResult
    {
        public string token { get; set; }
    }

    public class DtoUserAdminRegisterInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DtoUserStudentRegisterInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int? StudentId { get; set; }
    }

    public class DtoUserRegisterOutput
    {
        public string Username { get; set; }
    }

    public class DtoUserGet
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}