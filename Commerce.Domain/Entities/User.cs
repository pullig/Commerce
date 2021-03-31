using System;


namespace Commerce.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
