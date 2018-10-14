using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public User()
        {
            Blogs = new List<Blog>();
            Comments = new List<Comment>();
        }
    }
}
