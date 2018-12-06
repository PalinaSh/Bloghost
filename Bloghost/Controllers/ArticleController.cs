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
        public async Task<IActionResult> Create(ArticleModel articleModel, string tagDelete = "", bool delete = false, bool create = false)
        {
            if (delete)
            {
                DeleteTag(tagDelete, articleModel);
            }
            else if (articleModel.tagName != "")
            {
                AddTag(articleModel);   
            }
            if (ModelState.IsValid && create)
            {
                Blog blog = db.Blogs.FirstOrDefault(b => b.Id == articleModel.article.BlogId);
                articleModel.article.Blog = blog;
                blog.Articles.Add(articleModel.article);
                articleModel.article.Id = 0;
                db.Articles.Add(articleModel.article);

                await db.SaveChangesAsync();
                /*var articleId = articleModel.article.Id;

                var createdTags = db.Tags.ToList();
                foreach (var tagName in articleModel.tags.Split('$'))
                {
                    if (tagName != "" && createdTags.Where(t => t.Name == tagName).Count() == 0)
                    {
                        var tag = new Tag { Id = 0, Name = tagName };
                        db.Tags.Add(tag);
                        await db.SaveChangesAsync();
                        var tagId = tag.Id;
                        db.ArticleTags.Add(new ArticleTags { ArticleId = articleId, TagId = tagId });
                        await db.SaveChangesAsync();
                    }
                }*/
                
                return Redirect("Blog/Blog");
            }
            return View(articleModel);
        }

        private void DeleteTag(string tagDelete, ArticleModel articleModel)
        {
            List<string> tagsName = articleModel.tags.Split('$').ToList();
            tagsName.Remove(tagDelete);
            articleModel.tags = "";
            foreach (var tagName in tagsName)
            {
                if (tagName != "")
                {
                    articleModel.tags += tagName + "$";
                    articleModel.article.Tags.Add(new ArticleTags() { Tag = new Tag() { Id = 0, Name = tagName } });
                }
            }
        }

        private void AddTag(ArticleModel articleModel)
        {
            articleModel.tags += articleModel.tagName + "$";
            foreach (var tagName in articleModel.tags.Split('$'))
                if (tagName != "")
                {
                    var tag = new Tag() { Id = 0, Name = tagName };
                    if (articleModel.article.Tags.Where(t => t.Tag.Name == tagName).Count() == 0)
                        articleModel.article.Tags.Add(new ArticleTags() { Tag = tag });
                }
        }
    }
}
