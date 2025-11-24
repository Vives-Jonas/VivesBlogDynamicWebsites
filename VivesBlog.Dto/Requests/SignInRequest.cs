using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Requests
{
    public class SignInRequest
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
