namespace VivesBlog.Dto.Filter
{
    public class ArticleFilter
    {
        public string? Search { get; set; }

        public bool UseAuthorIdFilter { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        

    }
}
