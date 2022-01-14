using System;
using System.Collections.Generic;

#nullable disable

namespace NETApp.Models
{
    public partial class Twittor
    {
        public Twittor()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
