using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
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
                .Where(a => !authorId.HasValue || a.AuthorId == authorId.Value)
                .ProjectToResponse()
                .ToListAsync();
        }

        public async Task<ArticleResponse?> Get(int id)
        {
            return await dbContext.Articles
                .AsNoTracking()
                .ProjectToResponse()
                .FirstOrDefaultAsync(a => a.Id == id);

        }

        public async Task<IList<ArticleResponse>> GetRandom(int count = 5)
        {
            return await dbContext.Articles
                .AsNoTracking()
                .OrderBy(_ => EF.Functions.Random())
                .Take(count)
                .ProjectToResponse()
                .ToListAsync();
        }

        public async Task<ServiceResult<ArticleResponse>> Create(ArticleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Content))
            {
                return new ServiceResult<ArticleResponse>().Required(nameof(request.Title));
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

            var articleResponse = await Get(article.Id);

            return new ServiceResult<ArticleResponse>(articleResponse);
        }

        public async Task<ServiceResult<ArticleResponse>> Update(int id, ArticleRequest request)
        {
            var article = await dbContext.Articles.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return new ServiceResult<ArticleResponse>().NotFound(nameof(article));
            }

            article.Title = request.Title;
            article.Content = request.Content;
            article.UpdatedDate = DateTime.UtcNow;
            article.AuthorId = request.AuthorId;

           
            await dbContext.SaveChangesAsync();

            var articleResponse = await Get(article.Id);

            return new ServiceResult<ArticleResponse>(articleResponse);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var article = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);


            if (article == null)
            {
                return new ServiceResult().AlreadyRemoved();
            }

            dbContext.Articles.Remove(article);
            await dbContext.SaveChangesAsync();

            return new ServiceResult();
        }
    }
}
