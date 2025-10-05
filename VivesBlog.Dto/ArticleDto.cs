namespace VivesBlog.Dto
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Author { get; set; }
    }
}
