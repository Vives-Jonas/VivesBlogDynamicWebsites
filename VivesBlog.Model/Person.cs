using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Model
{
    public class Person
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();   
    }
}
