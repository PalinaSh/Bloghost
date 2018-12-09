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
            var tagModel = db.Tags.FirstOrDefault(t => t.Name == tag);
            IEnumerable<ArticleTags> articleTags = db.ArticleTags.Where(t => t.ArticleId == tagModel.Id); // потому что почему-то перепутаны колонки :З
            var articles = new List<Article>();
            try
            {
                foreach (var articleTag in articleTags)
                {
                    articles.Add(db.Articles.FirstOrDefault(a => a.Id == articleTag.TagId));
                    articles.Last().Tags = new List<ArticleTags>();
                    foreach (var tagArticle in db.ArticleTags.Where(a => a.TagId == articleTag.TagId))
                    {
                        var Tag = db.Tags.FirstOrDefault(t => t.Id == tagArticle.ArticleId);
                        if (articles.Last().Tags.Where(t => t.Tag.Name == Tag.Name).Count() == 0)
                            articles.Last().Tags.Add(new ArticleTags() { Tag = Tag });
                    }
                }
            }
            catch (Exception e) { return View("Articles", null); }
            return View("Articles", articles);
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
                
                return Redirect("~/Blog/Blog");
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
                    if (articleModel.tags.Split('$').Where(t => t == tagName).Count() == 0)
                        articleModel.tags += tagName + "$";
                    articleModel.article.Tags.Add(new ArticleTags() { Tag = new Tag() { Id = 0, Name = tagName } });
                }
            }
        }

        private void AddTag(ArticleModel articleModel)
        {
            if (articleModel.tags == null)
                articleModel.tags = "";
            if (articleModel.tags.Split('$').Where(t => t == articleModel.tagName).Count() == 0)
                articleModel.tags += articleModel.tagName + "$";
                foreach (var tagName in articleModel.tags.Split('$'))
                    if (tagName != "")
                    {
                        var tag = new Tag() { Id = 0, Name = tagName };
                        if (articleModel.article.Tags.Where(t => t.Tag.Name == tagName).Count() == 0)
                            articleModel.article.Tags.Add(new ArticleTags() { Tag = tag });
                    }
        }

        public async Task<IActionResult> Delete(int articleId, int blogId)
        {
            var article = db.Articles.FirstOrDefault(a => a.Id == articleId);
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return Redirect($"~/Blog/Blog/{blogId}");
        }

        public IActionResult Edit(int id)
        {
            var article = db.Articles.FirstOrDefault(a => a.Id == id);
            return View();
        }
    }
}
