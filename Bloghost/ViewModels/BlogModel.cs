using Bloghost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.ViewModels
{
    public class BlogModel
    {
        public Blog Blog { get; set; }
        public User User { get; set; }
    }
}
