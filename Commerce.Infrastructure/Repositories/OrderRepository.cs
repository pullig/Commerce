using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Context;

namespace Commerce.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(CommerceContext context) : base(context)
        {

        }
    }
}
