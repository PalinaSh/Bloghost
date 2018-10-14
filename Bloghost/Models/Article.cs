using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string NameArticle { get; set; }
        public string DescriptionArcticle { get; set; }
        [Required]
        public string TextArticle { get; set; }
        [Required]
        public DateTime CreationDateArticle { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        
        public ICollection<ArticleComments> Comments { get; set; }
        public ICollection<ArticleTags> Tags { get; set; }

        public Article()
        {
            Tags = new List<ArticleTags>();
            Comments = new List<ArticleComments>();
        }
    }
}
