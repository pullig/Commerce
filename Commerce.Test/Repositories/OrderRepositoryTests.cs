using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void GetOrders_ShouldReturnListOfOrdersFilteredByUserName()
        {
            var request = new GetOrderRequest
            {
                Username = "ausername"
            };

            var orderRepository = InitializeRepository();
            var orders = context.CommerceContext().Orders
                            .Where(u => u.User.Username.ToLower().Equals(request.Username.ToLower()));

            var result = orderRepository.GetOrders(request);

            context.DropCommerceContext();
            Assert.Equal(orders, result);
        }

        [Fact]
        public void GetOrders_ShouldReturnListOfOrdersFilteredByProductName()
        {
            var request = new GetOrderRequest
            {
                ProductName = "Product1"
            };

            var orderRepository = InitializeRepository();
            var orders = context.CommerceContext().Orders
                            .Where(u => u.Products.Any( p => p.Product.Name.ToLower().Equals(request.ProductName.ToLower())));

            var result = orderRepository.GetOrders(request);

            context.DropCommerceContext();
            Assert.Equal(orders, result);
        }

        [Fact]
        public void GetOrders_ShouldReturnListOfOrdersFilteredByOrderId()
        {
            var request = new GetOrderRequest
            {
                OrderId = 1
            };

            var orderRepository = InitializeRepository();
            var orders = context.CommerceContext().Orders
                            .Where(u => u.Id == request.OrderId);

            var result = orderRepository.GetOrders(request);

            context.DropCommerceContext();
            Assert.Equal(orders, result);
        }

        [Fact]
        public void GetOrders_ShouldReturnListOfOrdersFilteredByStartDate()
        {
            var request = new GetOrderRequest
            {
                StartDate = DateTime.Now.AddDays(-15)
            };

            var orderRepository = InitializeRepository();
            var orders = context.CommerceContext().Orders
                            .Where(u => u.CreationDate >= request.StartDate);

            var result = orderRepository.GetOrders(request);

            context.DropCommerceContext();
            Assert.Equal(orders, result);
        }

        [Fact]
        public void GetOrders_ShouldReturnListOfOrdersFilteredByAll()
        {
            var request = new GetOrderRequest
            {
                Username = "ausername",
                ProductName = "Product1",
                StartDate = DateTime.Now.AddDays(-50),
                EndDate = DateTime.Now.AddDays(1),
                OrderId = 1
            };

            var orderRepository = InitializeRepository();
            var orders = context.CommerceContext().Orders
                            .Where(u => u.User.Username.ToLower().Equals(request.Username.ToLower()) &&
                                            u.Products.Any(p => p.Product.Name.ToLower().Equals(request.ProductName.ToLower())) &&
                                            u.Id == request.OrderId &&
                                            u.CreationDate >= request.StartDate &&
                                            u.CreationDate <= request.EndDate);

            var result = orderRepository.GetOrders(request);

            context.DropCommerceContext();
            Assert.Equal(orders, result);

        }

        private IOrderRepository InitializeRepository()
        {
            context = new MockCommerceContext(dataBaseName);
            return new OrderRepository(context.CommerceContext());
        }
    }
}
