using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Responses
{
    public class ArticleResponse
    {
        public int Id { get; set; }


        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }



        public int? AuthorId { get; set; }
        
        public string? AuthorName { get; set; }
    }
}
