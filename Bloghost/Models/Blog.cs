using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    [DataContract]
    public class Blog
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [DataMember]
        public string NameBlog { get; set; }
        [DataMember]
        public string DescriptionBlog { get; set; }
        [DataMember]
        [Required]
        public DateTime CreationDateBlog { get; set; }

        [DataMember]
        public int? UserId { get; set; }
        public User User { get; set; }

        public List<Article> Articles { get; set; }

        public Blog()
        {
            Articles = new List<Article>();
        }
    }
}
