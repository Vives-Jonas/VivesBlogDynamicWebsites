using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Model;
using VivesBlog.Repository;

namespace VivesBlog.Services
{
    public class BlogService
    {

        private readonly BlogPostDbContext _dbContext;

        public BlogService(BlogPostDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IList<BlogPost> Find()
        {
            return _dbContext.BlogPosts
                .Include(b => b.Author)
                .ToList();
        }

        public BlogPost? Get(int id)
        {
            return _dbContext.BlogPosts
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);
        }

        public BlogPost? Create(BlogPost blogPost)
        {
            blogPost.CreatedDate = DateTime.Now;
            _dbContext.BlogPosts.Add(blogPost);
            _dbContext.SaveChanges();
            return blogPost;
        }

        public BlogPost? Update(int id, BlogPost blogPost)
        {
            var dbBlogPost = Get(id);

            if (dbBlogPost == null)
            {
                return null;
            }

            dbBlogPost.UpdatedDate = DateTime.Now;
            dbBlogPost.AuthorId = blogPost.AuthorId;
            dbBlogPost.Title = blogPost.Title;
            dbBlogPost.Content = blogPost.Content;

            _dbContext.SaveChanges();

            return dbBlogPost;
        }

        public void Delete(int id)
        {
            var blogPost = Get(id);
            if (blogPost == null)
            {
                return;
            }

            _dbContext.BlogPosts.Remove(blogPost);
            _dbContext.SaveChanges();
        }
    }
}
