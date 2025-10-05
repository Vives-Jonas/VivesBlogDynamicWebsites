using Microsoft.EntityFrameworkCore;
using VivesBlog.Dto;
using VivesBlog.Model;
using VivesBlog.Repository;
using VivesBlog.Services.Extensions;

namespace VivesBlog.Services
{
    public class BlogService(VivesBlogDbContext dbContext)
    {
        public async Task<IList<ArticleDto>> Find()
        {
            var blogPosts = await dbContext.BlogPosts
                .Include(b => b.Author)
                .ToListAsync();
            return blogPosts.Select(b => b.ToArticleDto()).ToList();
        }

        public async Task<ArticleDto?> Get(int id)
        {
            var blogPost = await dbContext.BlogPosts
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (blogPost == null)
            {
                return null;
            }
            return blogPost.ToArticleDto();
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
            var dbBlogPost = await dbContext.BlogPosts
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);


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
            var blogPost = await dbContext.BlogPosts
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blogPost == null)
            {
                return;
            }

            dbContext.BlogPosts.Remove(blogPost);
            await dbContext.SaveChangesAsync();
        }
    }
}
