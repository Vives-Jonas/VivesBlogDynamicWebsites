using Microsoft.EntityFrameworkCore;
using VivesBlog.Model;
using VivesBlog.Repository;

namespace VivesBlog.Services
{
    public class BlogService(VivesBlogDbContext dbContext)
    {
        public async Task<IList<BlogPost>> Find()
        {
            return await dbContext.BlogPosts
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<BlogPost?> Get(int id)
        {
            return await dbContext.BlogPosts
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogPost?> Create(BlogPost blogPost)
        {
            blogPost.CreatedDate = DateTime.Now;
            dbContext.BlogPosts.Add(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> Update(int id, BlogPost blogPost)
        {
            var dbBlogPost = await Get(id);

            if (dbBlogPost == null)
            {
                return null;
            }

            dbBlogPost.UpdatedDate = DateTime.Now;
            dbBlogPost.AuthorId = blogPost.AuthorId;
            dbBlogPost.Title = blogPost.Title;
            dbBlogPost.Content = blogPost.Content;

            await dbContext.SaveChangesAsync();

            return dbBlogPost;
        }

        public async Task Delete(int id)
        {
            var blogPost = await Get(id);
            if (blogPost == null)
            {
                return;
            }

            dbContext.BlogPosts.Remove(blogPost);
            await dbContext.SaveChangesAsync();
        }
    }
}
