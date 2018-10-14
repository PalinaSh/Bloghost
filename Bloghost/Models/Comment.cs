using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<ArticleComments> Articles { get; set; }

        public Comment()
        {
            Articles = new List<ArticleComments>();
        }
    }
}
