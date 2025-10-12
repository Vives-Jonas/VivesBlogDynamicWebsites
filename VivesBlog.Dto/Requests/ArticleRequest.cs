using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Requests
{
    public class ArticleRequest
    {
        
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Content { get; set; }

        public int? AuthorId { get; set; }
        
    }
}
