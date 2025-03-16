namespace VivesBlog.Mvc.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }

        public int AuthorId { get; set; }
        public Person? Author { get; set; }
    }
}
