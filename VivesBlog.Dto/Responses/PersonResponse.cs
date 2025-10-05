using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Responses
{
    public class PersonResponse
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
        public int NumberOfArticles { get; set; }
    }
}
