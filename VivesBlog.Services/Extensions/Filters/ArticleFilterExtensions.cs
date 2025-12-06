using VivesBlog.Model;
using VivesBlog.Dto.Filter;


namespace VivesBlog.Services.Extensions.Filters
{
    public static class ArticleFilterExtensions
    {
        public static IQueryable<Article> ApplyFilter(this IQueryable<Article> query, ArticleFilter? filter)
        {
            if (filter is null)
            {
                return query;
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchCriteria = filter.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var searchText in searchCriteria)
                {
                    var loweredSearchText = searchText.ToLowerInvariant();
                    query = query.Where(article =>
                        article.Title.ToLower().Contains(loweredSearchText)
                        || article.Content.ToLower().Contains(loweredSearchText)
                        || (article.Author != null && article.Author.FirstName.ToLower().Contains(loweredSearchText))
                        || (article.Author != null && article.Author.LastName.ToLower().Contains(loweredSearchText))
                        || (article.Author != null &&
                            (article.Author.FirstName.ToLower() + " " + article.Author.LastName.ToLower()).Contains(loweredSearchText)));
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.AuthorName))
            {
                var loweredAuthorName = filter.AuthorName.ToLowerInvariant();
                query = query.Where(article =>
                    article.Author != null &&
                    (article.Author.FirstName.ToLower().Contains(loweredAuthorName)
                     || article.Author.LastName.ToLower().Contains(loweredAuthorName)
                     || (article.Author.FirstName.ToLower() + " " + article.Author.LastName.ToLower()).Contains(loweredAuthorName)));
            }



            if (filter.UseAuthorIdFilter)
            {
                query = query.Where(article => article.AuthorId == filter.AuthorId);
            }


            return query;
        }
    }
}
