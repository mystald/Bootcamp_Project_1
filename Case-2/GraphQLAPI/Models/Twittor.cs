using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Models
{
    public class Twittor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}