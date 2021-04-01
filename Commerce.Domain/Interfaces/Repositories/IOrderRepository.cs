using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using System.Collections.Generic;

namespace Commerce.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Search for orders with the possibility of filetering by the username of the user that made the order
        /// the product that might be in the order, order id or creation date, if no filters are passed returns all orders
        /// </summary>
        /// <param name="request">Data to filter the search</param>
        /// <returns>List of orders</returns>
        public IEnumerable<Order> GetOrders(GetOrderRequest request);
    }
}
