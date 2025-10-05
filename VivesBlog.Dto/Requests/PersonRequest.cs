using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Requests
{
    public class PersonRequest
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        
    }
}
