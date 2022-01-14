using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Dtos
{
    public class DtoUserGet
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool isLocked { get; set; }

    }
}