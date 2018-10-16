using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bloghost.Models;
using Bloghost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloghost.Controllers
{
    public class UserController : Controller
    {
        private BlogContext db;
        private IHostingEnvironment _appEnvironment;
        private static User currentProfile;
        public UserController(BlogContext context, IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            return View(await db.Blogs.ToListAsync());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            currentProfile = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            UserModel model = new UserModel
            {
                Name = currentProfile.Name,
                Email = currentProfile.Email,
                Photo = currentProfile.Photo
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UserModel model)
        {

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoto(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/images/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                currentProfile.Photo = "~" + path;
            }

            db.Users.Update(currentProfile);
            await db.SaveChangesAsync();

            return View("Edit", new UserModel
            {
                Name = currentProfile.Name,
                Email = currentProfile.Email,
                Photo = currentProfile.Photo
            });
        }
    }
}