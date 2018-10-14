using Bloghost.Controllers;
using Bloghost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost
{
    public class SampleData
    {
        public static void Initialize(BlogContext context)
        {
            if (!context.Users.Any())
            {
                string adminRoleName = "admin";
                string userRoleName = "user";

                string adminEmail = "admin@mail.ru";
                string adminPassword = AccountController.HashPassword("qwerty123");

                Role adminRole = new Role { Name = adminRoleName };
                Role userRole = new Role { Name = userRoleName };

                context.Roles.Add(userRole);
                context.Roles.Add(adminRole);

                context.Users.Add(new User { Email = adminEmail, Password = adminPassword, Role = adminRole, Name = "Admin" });

                context.SaveChanges();
            }
        }
    }
}
