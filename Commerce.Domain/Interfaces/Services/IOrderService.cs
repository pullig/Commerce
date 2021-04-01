using Commerce.Domain.DTOs;
using System.Threading.Tasks;

namespace Commerce.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="request">Details of the order</param>
        /// <returns></returns>
        public Task AddOrderAsync(AddOrderRequest request);
    }
}
