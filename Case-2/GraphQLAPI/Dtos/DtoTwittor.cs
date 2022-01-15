using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Dtos
{
    public class DtoTwittorGet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public int CommentCount { get; set; }
    }

    public class DtoTwittorInput
    {
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}