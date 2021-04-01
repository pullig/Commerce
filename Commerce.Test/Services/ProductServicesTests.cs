using AutoMapper;
using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Domain.Interfaces.Services;
using Commerce.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Commerce.Test.Services
{
    public class ProductServicesTests
    {
        private readonly IProductService productService;

        private readonly Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();

        Product mockProduct;

        public ProductServicesTests()
        {
            mockProduct = new Product
            {
                CreationDate = DateTime.Now,
                Description = "description",
                Id = 1,
                Name = "name",
                Price = 2M
            };

            this.productService = new ProductService(mockProductRepository.Object,
                                                mockMapper.Object);


        }

        [Fact]
        public async Task AddProductAsync_ShouldReturnSuccess()
        {
            var dto = new AddProductRequest
            {
                Description = mockProduct.Description,
                Name = mockProduct.Name,
                Price = mockProduct.Price
            };

            mockMapper.Setup(m => m.Map<Product>(It.IsAny<AddProductRequest>()))
                .Returns(mockProduct);

            mockProductRepository.Setup(u => u.AddAsync(It.IsAny<Product>()))
                .Callback<Product>((product) =>
                {
                    Assert.Equal(dto.Description, product.Description);
                    Assert.Equal(dto.Name, product.Name);
                    Assert.Equal(dto.Price, product.Price);
                });

            await productService.AddAsync(dto);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldReturnSuccess()
        {
            var dto = new UpdateProductRequest
            {
                Description = "Updated " + mockProduct.Description,
                Name = "Updated " + mockProduct.Name,
                Price = mockProduct.Price + 1
            };

            mockProductRepository.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(mockProduct);

            mockProductRepository.Setup(u => u.UpdateAsync(It.IsAny<Product>()))
                .Callback<Product>((product) =>
                {
                    Assert.Equal(mockProduct.Id, product.Id);
                    Assert.Equal(dto.Description, product.Description);
                    Assert.Equal(dto.Name, product.Name);
                    Assert.Equal(dto.Price, product.Price);
                });

            await productService.UpdateAsync(mockProduct.Id, dto);
        }


    }
}
