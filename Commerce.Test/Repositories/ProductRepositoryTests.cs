using Commerce.Domain.Entities;
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

        public ProductRepositoryTests()
        {

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

        private IProductRepository InitializeRepository()
        {
            context = new MockCommerceContext();
            return new ProductRepository(context.CommerceContext());
        }
    }
}
