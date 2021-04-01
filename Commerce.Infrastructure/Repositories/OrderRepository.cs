using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Commerce.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(CommerceContext context) : base(context)
        {

        }

        public IEnumerable<Order> GetOrders(GetOrderRequest request)
        {
            var orders = context.Orders.AsQueryable();

            if (request.OrderId.HasValue)
            {
                orders = orders.Where(o => o.Id == request.OrderId);
            }

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                orders = orders.Where(o => o.User.Username.Equals(request.Username));
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                orders = orders.Where(o => o.Products.Any(p => p.Product.Name.Equals(request.ProductName)));
            }

            if (request.StartDate.HasValue)
            {
                orders = orders.Where(o => o.CreationDate.Date >= request.StartDate);
            }

            if (request.EndDate.HasValue)
            {
                orders = orders.Where(o => o.CreationDate.Date <= request.EndDate);
            }

            return orders;
        }
    }
}
