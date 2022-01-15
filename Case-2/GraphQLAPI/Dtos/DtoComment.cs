using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Dtos
{
    public class DtoCommentGet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TwittorId { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
    }

    public class DtoCommentInput
    {
        public int UserId { get; set; }
        public int TwittorId { get; set; }
        public string Content { get; set; }
    }
}