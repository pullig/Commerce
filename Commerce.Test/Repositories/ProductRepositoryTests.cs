using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Commerce.Test.Repositories
{
    public class ProductRepositoryTests
    {
        MockCommerceContext context;

        string dataBaseName;

        public ProductRepositoryTests()
        {
            dataBaseName = "ProductRepositoryTests";
        }

        [Fact]
        public async Task AddAsync_ShouldAddProduct()
        {
            var productRepository = InitializeRepository();

            var product = new Product
            {
                CreationDate = DateTime.Now,
                Description = "newDescription",
                Name = "Product add test",
                Price = 2M
            };

            var result = await productRepository.AddAsync(product);

            var addedProduct = context.CommerceContext().Products.FirstOrDefault(u => u.Name.Equals(product.Name));

            context.DropCommerceContext();
            Assert.NotNull(addedProduct);
            Assert.NotEqual(0, addedProduct.Id);
            Assert.Equal(product.Name, addedProduct.Name);
            Assert.Equal(product.Description, addedProduct.Description);
            Assert.Equal(product.Price, addedProduct.Price);
            Assert.Equal(product.CreationDate, addedProduct.CreationDate);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct()
        {
            var productRepository = InitializeRepository();

            var product = context.CommerceContext().Products.FirstOrDefault(u => u.Id == 1);

            product.Name = "updatedName";
            product.Description = "updatedDescription";
            product.Price = 6M;

            var result = await productRepository.UpdateAsync(product);

            var updatedProduct = context.CommerceContext().Products.FirstOrDefault(u => u.Id == product.Id);

            context.DropCommerceContext();
            Assert.NotNull(updatedProduct);
            Assert.Equal(product.Id, updatedProduct.Id);
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(product.Description, updatedProduct.Description);
            Assert.Equal(product.Price, updatedProduct.Price);
            Assert.Equal(product.CreationDate, updatedProduct.CreationDate);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfProductsFilteredByName()
        {
            var dto = new GetProductsRequest
            {
                Name = "product",
                OrderBy = ProductOrderBy.NameAscending
            };

            var productRepository = InitializeRepository();
            var products = context.CommerceContext().Products
                            .Where(u => u.Name.ToLower().Contains(dto.Name.ToLower()))
                            .OrderBy(u => u.Name);

            var result = productRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(products, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfProductsFilteredByDescription()
        {
            var dto = new GetProductsRequest
            {
                Description = "Description",
                OrderBy = ProductOrderBy.NameDescending
            };

            var productRepository = InitializeRepository();
            var products = context.CommerceContext().Products
                            .Where(u => u.Description.ToLower().Contains(dto.Description.ToLower()))
                            .OrderByDescending(u => u.Name);

            var result = productRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(products, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfProductsFilteredByPrice()
        {
            var dto = new GetProductsRequest
            {
                Price = 10.20M,
                OrderBy = ProductOrderBy.DescriptionAscending
            };

            var productRepository = InitializeRepository();
            var products = context.CommerceContext().Products
                            .Where(u => u.Price == dto.Price)
                            .OrderBy(u => u.Description);

            var result = productRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(products, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfProductsFilteredByStartDate()
        {
            var dto = new GetProductsRequest
            {
                StartDate = DateTime.Now.AddDays(-15),
                OrderBy = ProductOrderBy.DescriptionDescending
            };

            var productRepository = InitializeRepository();
            var products = context.CommerceContext().Products
                            .Where(u => u.CreationDate >= dto.StartDate)
                            .OrderByDescending(u => u.Description);

            var result = productRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(products, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfProductsFilteredByEndDate()
        {
            var dto = new GetProductsRequest
            {
                EndDate = DateTime.Now.AddDays(-15),
                OrderBy = ProductOrderBy.NameAscending
            };

            var productRepository = InitializeRepository();
            var products = context.CommerceContext().Products
                            .Where(u => u.Name.Contains(dto.Name) &&
                                            u.Description.Contains(dto.Description))
                            .OrderBy(u => u.Name);

            var result = productRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(products, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfProductsFilteredByAll()
        {
            var dto = new GetProductsRequest
            {
                Name = "product",
                Description = "description",
                StartDate = DateTime.Now.AddDays(-50),
                EndDate = DateTime.Now.AddDays(1),
                Price = 10.20M,
                OrderBy = ProductOrderBy.NameAscending
            };

            var productRepository = InitializeRepository();
            var products = context.CommerceContext().Products
                            .Where(u => u.Name.ToLower().Contains(dto.Name.ToLower()) &&
                                            u.Description.ToLower().Contains(dto.Description.ToLower()) &&
                                            u.Price == dto.Price &&
                                            u.CreationDate >= dto.StartDate &&
                                            u.CreationDate <= dto.EndDate)
                            .OrderBy(u => u.Name);

            var result = productRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(products, result);

        }

        private IProductRepository InitializeRepository()
        {
            context = new MockCommerceContext(dataBaseName);
            return new ProductRepository(context.CommerceContext());
        }
    }
}
