using Commerce.Domain.DTOs;
using System.Collections.Generic;
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
        /// <summary>
        /// Search for orders with the possibility of filetering by the username of the user that made the order
        /// the product that might be in the order, order id or creation date, if no filters are passed returns all orders
        /// </summary>
        /// <param name="request">Data to filter the search</param>
        /// <returns>List of orders</returns>
        public IEnumerable<GetOrderResult> GetOrders(GetOrderRequest request);
    }
}
