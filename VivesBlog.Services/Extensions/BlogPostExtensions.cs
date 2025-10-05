using VivesBlog.Dto;
using VivesBlog.Model;

namespace VivesBlog.Services.Extensions
{
    public static class BlogPostExtensions
    {
        public static ArticleDto ToArticleDto(this BlogPost blogPost)
        {
            return new ArticleDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                CreatedDate = blogPost.CreatedDate,
                UpdatedDate = blogPost.CreatedDate,
                Author = $"{blogPost.Author?.FirstName} {blogPost.Author?.LastName}" ?? "Unknown Author"
            };
        }
    }
}
