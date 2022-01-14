using System;
using System.Collections.Generic;

#nullable disable

namespace NETApp.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TwittorId { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public virtual Twittor Twittor { get; set; }
        public virtual User User { get; set; }
    }
}
