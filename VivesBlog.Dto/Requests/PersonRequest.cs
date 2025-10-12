using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Requests
{
    public class PersonRequest
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        
    }
}
