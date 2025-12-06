namespace VivesBlog.Dto.Results
{
    public class ArticleResult
    {
        public int Id { get; set; }
        
        public required string Title { get; set; }
        public required string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}
