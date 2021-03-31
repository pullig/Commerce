using Newtonsoft.Json;

namespace Commerce.Domain.DTOs
{
    public class Auth0TokenPost
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty("audience")]
        public string Audience { get; set; }
        [JsonProperty("grant_type")]
        public string GrantType => "client_credentials";
    }
}
