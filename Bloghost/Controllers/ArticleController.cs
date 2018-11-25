using Bloghost.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Controllers
{
    public class ArticleController: Controller
    {
        private BlogContext db;
        public ArticleController(BlogContext context)
        {
            db = context;
        }

        public IActionResult Search(string tag)
        {
            IEnumerable<ArticleTags> articleTags = db.ArticleTags.Where(t => t.Tag.Name == tag);
            return View("Articles", articleTags);
        }

        public IActionResult Create(Article article)
        {
            return View();
        }
    }
}
