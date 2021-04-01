using AutoMapper;
using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task AddOrderAsync(AddOrderRequest request)
        {
            var user = userRepository.GetById(request.UserId);

            if (user == null)
            {
                throw new Exception(ErrorMessage.UserNotFound);
            }

            var productsList = productRepository.GetProducts(request.Products.Select(p => p.ProductId));

            if(productsList == null || productsList.Count() == 0)
            {
                throw new Exception(ErrorMessage.ProductsNotFound);
            }

            var order = mapper.Map<Order>(request);
            order.CreationDate = DateTime.Now;

            foreach(var product in order.Products)
            {
                var actualProduct = productsList.First(p => p.Id == product.ProductId);
                product.UnityPrice = actualProduct.Price;
            }

            await orderRepository.AddAsync(order);

        }

        public IEnumerable<GetOrderResult> GetOrders(GetOrderRequest request)
        {
            var orders = orderRepository.GetOrders(request);

            var result = mapper.Map<IEnumerable<GetOrderResult>>(orders.ToList());

            return result;
        }
    }
}
