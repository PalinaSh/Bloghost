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
            return View(articleModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel articleModel)
        {
            if (articleModel.tagName != "")
            {
                
                articleModel.tags += articleModel.tagName + "$";
                foreach (var tagName in articleModel.tags.Split('$'))
                    if (tagName != "")
                    { 
                        var tag = new Tag() { Id = 0, Name = tagName };
                        articleModel.article.Tags.Add(new ArticleTags() { Tag = tag });
                    }
               
            }
            if (ModelState.IsValid)
            {
                Blog blog = db.Blogs.FirstOrDefault(b => b.Id == articleModel.article.BlogId);
                articleModel.article.Blog = blog;
                blog.Articles.Add(articleModel.article);
                articleModel.article.Id = 0;
                db.Articles.Add(articleModel.article);
                await db.SaveChangesAsync();
                return Redirect("Blog/Blog");
                //return View("Blog/Blog");
            }
            return View(articleModel);
        }

        /*[HttpPost]
        public IActionResult AddTagString(ArticleModel articleModel)
        {
            var tag = new Tag() {Id = 0, Name = articleModel.tagName };
            articleModel.article.Tags.Add(new ArticleTags(){Tag = tag});
            return View($"Create/{articleModel.article.BlogId}", articleModel);
        }*/
    }
}
