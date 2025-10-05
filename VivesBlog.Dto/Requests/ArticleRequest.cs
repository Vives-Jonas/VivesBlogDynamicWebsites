using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Requests
{
    public class ArticleRequest
    {
        
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public required string Content { get; set; }


        public int? AuthorId { get; set; }
        
    }
}
