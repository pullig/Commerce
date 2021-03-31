using Commerce.Domain.DTOs;
using System.Threading.Tasks;

namespace Commerce.Domain.Interfaces.Clients
{
    public interface IAuth0Client
    {
        public Task<GetTokenResult> GetTokenAsync();
    }
}
