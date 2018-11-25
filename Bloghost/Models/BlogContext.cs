using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTags> ArticleTags { get; set; }
        public DbSet<ArticleComments> ArticleComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTags>().HasKey(x => new { x.ArticleId, x.TagId });
            modelBuilder.Entity<ArticleTags>().HasOne(x => x.Article).WithMany(y => y.Tags).HasForeignKey(y => y.TagId);
            modelBuilder.Entity<ArticleTags>().HasOne(x => x.Tag).WithMany(y => y.Articles).HasForeignKey(y => y.ArticleId);

            modelBuilder.Entity<ArticleComments>().HasKey(x => new { x.ArticleId, x.CommentId });
            modelBuilder.Entity<ArticleComments>().HasOne(x => x.Article).WithMany(y => y.Comments).HasForeignKey(y => y.CommentId);
            modelBuilder.Entity<ArticleComments>().HasOne(x => x.Comment).WithMany(y => y.Articles).HasForeignKey(y => y.ArticleId);
        }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
