using AutoMapper;
using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Domain.Interfaces.Services;
using Commerce.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Commerce.Test.Services
{
    public class OrderServiceTests
    {
        private readonly IOrderService orderService;

        private readonly Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
        private readonly Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

        Order mockOrder;
        ProductOrder mockProductOrder;
        User mockUser;
        Product mockProduct;

        public OrderServiceTests()
        {
            mockUser = new User()
            {
                CreationDate = DateTime.Now,
                DisplayName = "displayName",
                EmailAddress = "emailAddress",
                Id = 1,
                Password = "password",
                Username = "username"
            };

            mockProduct = new Product
            {
                CreationDate = DateTime.Now,
                Description = "description",
                Id = 1,
                Name = "name",
                Price = 1M
            };

            mockProductOrder = new ProductOrder
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 10,
                UnityPrice = 1M,
                Product = mockProduct
            };

            mockOrder = new Order
            {
                CreationDate = DateTime.Now,
                Id = 1,
                Products = new List<ProductOrder>
                {
                    mockProductOrder
                },
                UserId = 1,
                User = mockUser
            };

            orderService = new OrderService(mockOrderRepository.Object,
                mockProductRepository.Object, mockUserRepository.Object, 
                mockMapper.Object);
        }

        [Fact]
        public async Task AddOrderAsync_ShoudReturnSuccess()
        {
            var request = new AddOrderRequest
            {
                UserId = mockOrder.UserId,
                Products = new List<AddOrderProductRequest>
                {
                    new AddOrderProductRequest
                    {
                        ProductId = mockProductOrder.ProductId,
                        Quantity = mockProductOrder.Quantity
                    }
                }
            };

            var listProducts = new List<Product>
            {
                mockProduct
            };

            mockUserRepository.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(mockUser);

            mockProductRepository.Setup(m => m.GetProducts(It.IsAny<IEnumerable<int>>()))
                .Returns(listProducts);

            mockMapper.Setup(m => m.Map<Order>(It.IsAny<AddOrderRequest>()))
                .Returns(mockOrder);

            mockOrderRepository.Setup(u => u.AddAsync(It.IsAny<Order>()))
                .Callback<Order>((order) =>
                {
                    Assert.Equal(request.UserId, order.UserId);
                    Assert.Equal(request.Products.Count, order.Products.Count);
                    Assert.Equal(request.Products.Select(p => p.ProductId), order.Products.Select(p => p.ProductId));
                    Assert.Equal(request.Products.Select(p => p.Quantity), order.Products.Select(p => p.Quantity));
                });

            await orderService.AddOrderAsync(request);
        }

        [Fact]
        public async Task AddOrderAsync_ShoudThrowExceptionNoProduct()
        {
            var request = new AddOrderRequest
            {
                UserId = mockOrder.UserId,
                Products = new List<AddOrderProductRequest>
                {
                    new AddOrderProductRequest
                    {
                        ProductId = mockProductOrder.ProductId,
                        Quantity = mockProductOrder.Quantity
                    }
                }
            };

            var listProducts = new List<Product>
            {
                mockProduct
            };

            mockUserRepository.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(mockUser);

            await Assert.ThrowsAsync<Exception>(() => orderService.AddOrderAsync(request));
        }

        [Fact]
        public async Task AddOrderAsync_ShoudThrowExceptionNoUser()
        {
            var request = new AddOrderRequest
            {
                UserId = mockOrder.UserId,
                Products = new List<AddOrderProductRequest>
                {
                    new AddOrderProductRequest
                    {
                        ProductId = mockProductOrder.ProductId,
                        Quantity = mockProductOrder.Quantity
                    }
                }
            };

            var listProducts = new List<Product>
            {
                mockProduct
            };

            await Assert.ThrowsAsync<Exception>(() => orderService.AddOrderAsync(request));
        }

        [Fact]
        public void GetOrders_ShourReturnListOfOrders()
        {
            var expected = GenerateOrderResult();
            var listOrders = new List<Order>
            {
                mockOrder
            };

            mockOrderRepository.Setup(m => m.GetOrders(It.IsAny<GetOrderRequest>()))
                .Returns(listOrders);

            mockMapper.Setup(m => m.Map<IEnumerable<GetOrderResult>>(It.IsAny<List<Order>>()))
                .Returns(expected);

            var result = orderService.GetOrders(new GetOrderRequest());

            Assert.Equal(expected, result);
        }

        private List<GetOrderResult> GenerateOrderResult()
        {
            return new List<GetOrderResult>
            {
                new GetOrderResult
                {
                    Id = mockOrder.Id,
                    Products = new List<GetProductOrdersResult>
                    {
                        new GetProductOrdersResult
                        {
                            Product = new GetProductsResult
                            {
                                CreationDate = mockProduct.CreationDate,
                                Description = mockProduct.Description,
                                Id = mockProduct.Id,
                                Name = mockProduct.Name,
                                Price = mockProduct.Price
                            },
                            Quantity = mockProductOrder.Quantity,
                            UnityPrice = mockProductOrder.UnityPrice
                        }
                    },
                    User = new GetUserResult
                    {
                        CreationDate = mockUser.CreationDate,
                        DisplayName = mockUser.DisplayName,
                        EmailAddress = mockUser.EmailAddress,
                        Username = mockUser.Username
                    },
                    CreationDate = mockOrder.CreationDate
                }
            };
        }
    }
}
