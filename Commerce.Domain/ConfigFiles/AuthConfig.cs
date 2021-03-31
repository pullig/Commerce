namespace Commerce.Domain.ConfigFiles
{
    public class AuthConfig
    {
        public string Domain { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string TokenAddress { get; set; }
    }
}
