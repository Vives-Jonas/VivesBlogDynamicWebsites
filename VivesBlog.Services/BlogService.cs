using Microsoft.EntityFrameworkCore;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Responses;
using VivesBlog.Model;
using VivesBlog.Repository;
using VivesBlog.Services.Extensions;

namespace VivesBlog.Services
{
    public class BlogService(VivesBlogDbContext dbContext)
    {
        public async Task<IList<ArticleResponse>> Find(int? authorId = null)
        {
            return await dbContext.Articles
                .AsNoTracking()
                .Include(a => a.Author)
                .Where(a => !authorId.HasValue || a.AuthorId == authorId.Value)
                .ProjectToResponse()
                .ToListAsync();
        }

        public async Task<ArticleResponse?> Get(int id)
        {
            return await dbContext.Articles
                .AsNoTracking()
                .Include(a => a.Author)
                .ProjectToResponse()
                .FirstOrDefaultAsync(a => a.Id == id);

        }

        public async Task<ArticleResponse?> Create(ArticleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Content))
            {
                return null;
            }

            var article = new Article
            {
                Title = request.Title,
                Content = request.Content,
                CreatedDate = DateTime.UtcNow,
                AuthorId = request.AuthorId
            };

            
            dbContext.Articles.Add(article);
            await dbContext.SaveChangesAsync();
            return await Get(article.Id);
        }

        public async Task<ArticleResponse?> Update(int id, ArticleRequest request)
        {
            var article = await dbContext.Articles.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return null;
            }

            article.Title = request.Title;
            article.Content = request.Content;
            article.UpdatedDate = DateTime.UtcNow;
            article.AuthorId = request.AuthorId;

           
            await dbContext.SaveChangesAsync();

            return await Get(article.Id);
        }

        public async Task Delete(int id)
        {
            var blogPost = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);


            if (blogPost == null)
            {
                return;
            }

            dbContext.Articles.Remove(blogPost);
            await dbContext.SaveChangesAsync();
        }
    }
}
