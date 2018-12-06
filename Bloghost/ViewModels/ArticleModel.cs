using Bloghost.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.ViewModels
{
    public class ArticleModel
    {
        public Article article { get; set; }
        [Required(ErrorMessage = "Tag name is required")]
        public string tagName { get; set; }
        public string tags { get; set; }
        public string comment { get; set; }
    }
}
