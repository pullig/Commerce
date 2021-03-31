using System;
using System.ComponentModel.DataAnnotations;

namespace Commerce.Domain.DTOs
{
    public class SignUpRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }


    }
}
