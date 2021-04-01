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
            var userRepository = InitializeRepository();

            var product = new Product
            {
                CreationDate = DateTime.Now,
                Description = "newDescription",
                Name = "Product add test",
                Price = 2M
            };

            var result = await userRepository.AddAsync(product);

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
            var userRepository = InitializeRepository();

            var product = context.CommerceContext().Products.FirstOrDefault(u => u.Id == 1);

            product.Name = "updatedName";
            product.Description = "updatedDescription";
            product.Price = 6M;

            var result = await userRepository.UpdateAsync(product);

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
        public void GetProducts_ShouldReturnListOfUsersFilteredByName()
        {
            var dto = new GetProductsRequest
            {
                Name = "product",
                OrderBy = ProductOrderBy.NameAscending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Products
                            .Where(u => u.Name.Contains(dto.Name))
                            .OrderBy(u => u.Name);

            var result = userRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfUsersFilteredByDescription()
        {
            var dto = new GetProductsRequest
            {
                Description = "Description",
                OrderBy = ProductOrderBy.NameDescending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Products
                            .Where(u => u.Description.Contains(dto.Description))
                            .OrderByDescending(u => u.Name);

            var result = userRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfUsersFilteredByEmailAddress()
        {
            var dto = new GetProductsRequest
            {
                Price = 10.20M,
                OrderBy = ProductOrderBy.DescriptionAscending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Products
                            .Where(u => u.Price == dto.Price)
                            .OrderBy(u => u.Description);

            var result = userRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfUsersFilteredByStartDate()
        {
            var dto = new GetProductsRequest
            {
                StartDate = DateTime.Now,
                OrderBy = ProductOrderBy.DescriptionDescending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Products
                            .Where(u => u.CreationDate >= dto.StartDate)
                            .OrderByDescending(u => u.Description);

            var result = userRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfUsersFilteredByEndDate()
        {
            var dto = new GetProductsRequest
            {
                EndDate = DateTime.Now.AddDays(-15),
                OrderBy = ProductOrderBy.NameAscending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Products
                            .Where(u => u.Name.Contains(dto.Name) &&
                                            u.Description.Contains(dto.Description))
                            .OrderBy(u => u.Name);

            var result = userRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetProducts_ShouldReturnListOfUsersFilteredByAll()
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

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Products
                            .Where(u => u.Name.Contains(dto.Name) &&
                                            u.Description.Contains(dto.Description) &&
                                            u.Price == dto.Price &&
                                            u.CreationDate >= dto.StartDate &&
                                            u.CreationDate <= dto.EndDate)
                            .OrderBy(u => u.Name);

            var result = userRepository.GetProducts(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);

        }

        private IProductRepository InitializeRepository()
        {
            context = new MockCommerceContext(dataBaseName);
            return new ProductRepository(context.CommerceContext());
        }
    }
}
