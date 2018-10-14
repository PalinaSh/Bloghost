using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ArticleTags> Articles { get; set; }

        public Tag()
        {
            Articles = new List<ArticleTags>();
        }

    }
}
