using AutoMapper;
using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task AddAsync(AddProductRequest request)
        {
            var product = mapper.Map<Product>(request);

            product.CreationDate = DateTime.Now;

            await productRepository.AddAsync(product);
        }
    }
}
