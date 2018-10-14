using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    public class ArticleComments
    {
        public int ArticleId { get; set; }
        public int CommentId { get; set; }
        public Article Article { get; set; }
        public Comment Comment { get; set; }
    }
}
