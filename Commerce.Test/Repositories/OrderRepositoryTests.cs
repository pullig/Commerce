using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Commerce.Test.Repositories
{
    public class OrderRepositoryTests
    {
        MockCommerceContext context;

        string dataBaseName;

        public OrderRepositoryTests()
        {
            dataBaseName = "OrderRepositoryTests";
        }

        [Fact]
        public async Task AddAsync_ShouldAddOrder()
        {
            var orderRepository = InitializeRepository();

            var order = new Order
            {
                CreationDate = DateTime.Now,
                UserId = 1,
                Products = new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        ProductId = 1,
                        UnityPrice = 10.20M,
                        Quantity = 10
                    }
                }
            };

            var result = await orderRepository.AddAsync(order);

            context.DropCommerceContext();
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(order.UserId, result.UserId);
            Assert.NotEmpty(result.Products);
        }

        private IOrderRepository InitializeRepository()
        {
            context = new MockCommerceContext(dataBaseName);
            return new OrderRepository(context.CommerceContext());
        }
    }
}
