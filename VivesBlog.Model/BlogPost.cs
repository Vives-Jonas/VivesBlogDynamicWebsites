using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Model 
{
    public class BlogPost
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


        
        public int? AuthorId { get; set; }
        public Person? Author { get; set; }
    }
}
