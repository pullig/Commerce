using System;

namespace Commerce.Domain.DTOs
{
    public class SignedUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreationDate { get; set; }
    }
}