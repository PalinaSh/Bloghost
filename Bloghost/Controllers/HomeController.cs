using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bloghost.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Bloghost.ViewModels;

namespace Bloghost.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db;
        public HomeController(BlogContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await db.Blogs.ToListAsync();
            List<BlogModel> models = new List<BlogModel>();
            foreach (var blog in blogs)
            {
                BlogModel model = new BlogModel { Blog = blog, User = db.Users.FirstOrDefault(u => blog.UserId == u.Id) };
                models.Add(model);
            }
            return View(models);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
