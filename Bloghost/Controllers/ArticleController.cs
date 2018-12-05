using Bloghost.Models;
using Bloghost.ViewModels;
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

        [HttpGet]
        public IActionResult Create(int id)
        {
            var article = new Article();
            var blog = db.Blogs.FirstOrDefault(b => b.Id == id);
            article.Blog = new Blog { NameBlog = blog.NameBlog, Id = id };
            article.BlogId = id;
            var articleModel = new ArticleModel() { article = article };
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article)
        {
            if (ModelState.IsValid)
            {
                Blog blog = db.Blogs.FirstOrDefault(b => b.Id == article.BlogId);
                article.Blog = blog;
                blog.Articles.Add(article);
                article.Id = 0;
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return View("Blog/Blog");
            }
            return View(article);
        }

        [HttpPost]
        public IActionResult AddTagString(Article article)
        {

            return View("Create", article);
        }
    }
}
