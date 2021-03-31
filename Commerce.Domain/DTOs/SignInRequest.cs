using System.ComponentModel.DataAnnotations;

namespace Commerce.Domain.DTOs
{
    public class SignInRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
