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
}