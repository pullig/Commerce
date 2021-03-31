using Commerce.Domain.ConfigFiles;
using Commerce.Domain.DTOs;
using Commerce.Domain.Interfaces.Clients;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace Commerce.Infrastructure.Clients
{
    public class Auth0Client : IAuth0Client
    {
        private readonly IOptions<AuthConfig> config;


        public Auth0Client(IOptions<AuthConfig> config)
        {
            this.config = config;
        }

        public async Task<GetTokenResult> GetTokenAsync()
        {
            var data = new Auth0TokenPost
            {
                Audience = config.Value.Audience,
                ClientSecret = config.Value.Secret,
                ClientId = config.Value.ClientId
            };

            var myContent = JsonConvert.SerializeObject(data);

            var client = new RestClient(config.Value.TokenAddress);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", myContent, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            var result = JsonConvert.DeserializeObject<GetTokenResult>(response.Content);

            return result;
        }
    }
}
