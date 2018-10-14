using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string NameBlog { get; set; }
        public string DescriptionBlog { get; set; }
        [Required]
        public DateTime CreationDateBlog { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Article> Articles { get; set; }

        public Blog()
        {
            Articles = new List<Article>();
        }
    }
}
