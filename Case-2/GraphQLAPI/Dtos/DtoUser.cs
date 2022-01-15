using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Dtos
{
    public class DtoUserAuth
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DtoUserGet
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public bool isLocked { get; set; }

    }

    public class DtoUserInput
    {
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
    }

    public class DtoUserEditProfile
    {
        public string Username { get; set; }
        public string Bio { get; set; }
    }

    public class DtoUserChangePass
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}