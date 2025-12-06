using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Filter;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using VivesBlog.Model;
using VivesBlog.Repository;
using VivesBlog.Services.Extensions;
using VivesBlog.Services.Extensions.Filters;

namespace VivesBlog.Services
{
    public class BlogService(VivesBlogDbContext dbContext)
    {
        public async Task<FilteredPagedServiceResult<ArticleResult, ArticleFilter>> Find(Paging paging, string? sorting, ArticleFilter? filter)
        {
            var query = dbContext.Articles.AsNoTracking().Include(a => a.Author).ApplyFilter(filter);
            var totalCount = await query.CountAsync();

            sorting ??= $"{nameof(ArticleResult.CreatedDate)} desc";

            var articles = await query                
                .OrderBy(sorting)
                .ApplyPaging(paging)
                .ProjectToResult()
                .ToListAsync();

            return new FilteredPagedServiceResult<ArticleResult, ArticleFilter>
            {
                Data = articles,
                TotalCount = totalCount,
                Paging = paging,
                Sorting = sorting,
                Filter = filter
            };
        }

        public async Task<ArticleResult?> Get(int id)
        {
            return await dbContext.Articles
                .AsNoTracking()
                .ProjectToResult()
                .FirstOrDefaultAsync(a => a.Id == id);

        }

        public async Task<IList<ArticleResult>> GetRandom(int count = 5)
        {
            return await dbContext.Articles
                .AsNoTracking()
                .OrderBy(_ => EF.Functions.Random())
                .Take(count)
                .ProjectToResult()
                .ToListAsync();
        }

        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
            var serviceResult = new ServiceResult<ArticleResult>();
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                serviceResult.Required(nameof(request.Title));
            }
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                serviceResult.Required(nameof(request.Content));
            }
            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
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

            return new ServiceResult<ArticleResult>(articleResponse);
        }

        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
            var article = await dbContext.Articles.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return new ServiceResult<ArticleResult>().NotFound(nameof(article));
            }

            var serviceResult = new ServiceResult<ArticleResult>();
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                serviceResult.Required(nameof(request.Title));
            }
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                serviceResult.Required(nameof(request.Content));
            }
            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            article.Title = request.Title;
            article.Content = request.Content;
            article.UpdatedDate = DateTime.UtcNow;
            article.AuthorId = request.AuthorId;

           
            await dbContext.SaveChangesAsync();

            var articleResponse = await Get(article.Id);

            return new ServiceResult<ArticleResult>(articleResponse);
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
