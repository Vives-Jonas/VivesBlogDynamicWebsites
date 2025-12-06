using VivesBlog.Dto.Results;
using VivesBlog.Model;

namespace VivesBlog.Services.Extensions
{
    public static class ProjectionExtensions
    {
        public static IQueryable<ArticleResult> ProjectToResult(this IQueryable<Article> query)
        {
            return query.Select(a => new ArticleResult
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                AuthorId = a.AuthorId,
                AuthorName = a.Author != null
                    ? $"{a.Author.FirstName} {a.Author.LastName}"
                    : null,
            });


        }

        public static IQueryable<PersonResult> ProjectToResult(this IQueryable<Person> query)
        {
            return query.Select(p => new PersonResult
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                NumberOfArticles = p.Articles.Count
            });
        }
    }
}
