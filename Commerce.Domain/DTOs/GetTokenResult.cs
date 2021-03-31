using Newtonsoft.Json;

namespace Commerce.Domain.DTOs
{
    public class GetTokenResult
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}
