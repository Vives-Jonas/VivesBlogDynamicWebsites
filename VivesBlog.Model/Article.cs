namespace VivesBlog.Model 
{
    public class Article
    {
        public int Id { get; set; }


        
        public required string Title { get; set; }

        
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


        
        public int? AuthorId { get; set; }
        
        public Person? Author { get; set; }
    }
}
