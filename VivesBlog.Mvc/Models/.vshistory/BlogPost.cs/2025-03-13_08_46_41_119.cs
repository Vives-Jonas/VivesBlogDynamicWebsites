namespace VivesBlog.Mvc.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? Author { get; set; }
    }
}
