using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bloghost.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db;
        public BlogController(BlogContext context)
        {
            db = context;
        }

        public async Task<JsonResult> Create(string name, string description)
        {
            if (name is null)
                throw new Exception("Name is required");
            User currentProfile = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            Blog blog = new Blog
            {
                NameBlog = name,
                DescriptionBlog = description,
                CreationDateBlog = DateTime.Now,
                UserId = currentProfile.Id
            };
            db.Blogs.Add(blog);
            await db.SaveChangesAsync();

            Blog[] blogs = db.Blogs.Where(b => b.UserId == currentProfile.Id).ToArray();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Blog[]));
            using (MemoryStream fs = new MemoryStream())
            {
                jsonFormatter.WriteObject(fs, blogs);
                return Json(Encoding.Default.GetString(fs.ToArray()));
            }
        }

        public IActionResult Blog(int id)
        {
            var blog = db.Blogs.FirstOrDefault(b => b.Id == id);
            var user = db.Users.FirstOrDefault(u => u.Id == blog.UserId);
            var articles = db.Articles.Where(a => a.BlogId == id);
            blog.User = user;
            blog.Articles.AddRange(articles);
            return View(blog);
        }
    }
}