using Commerce.Domain.Entities;
using Commerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Commerce.Test
{
    public class MockCommerceContext
    {
        readonly CommerceContext context;

        public MockCommerceContext(string dataBasename)
        {
            var options = new DbContextOptionsBuilder<CommerceContext>()
                .UseInMemoryDatabase(databaseName: dataBasename)
                .Options;

            context = new CommerceContext(options);

            context.Users.Add(new User { Id = 1, Username = "ausername", DisplayName = "aDisplayName", CreationDate = DateTime.Now, EmailAddress = "aemail@email.com", Password = "c3RyaW5n" });
            context.Users.Add(new User { Id = 2, Username = "username2", DisplayName = "DisplayName2", CreationDate = DateTime.Now.AddDays(-50), EmailAddress = "email2@email.com", Password = "c3RyaW5n" });
            context.Users.Add(new User { Id = 3, Username = "musername3", DisplayName = "mDisplaName3", CreationDate = DateTime.Now.AddYears(-1), EmailAddress = "memail3@email.com", Password = "c3RyaW5n" });

            context.Orders.Add(new Order { Id = 1, UserId = 1 });
            context.Orders.Add(new Order { Id = 2, UserId = 2 });
            context.Orders.Add(new Order { Id = 3, UserId = 3 });

            context.Products.Add(new Product { Id = 1, CreationDate = DateTime.Now, Description = "Product description 1", Name = "Product1", Price = 10.20M });
            context.Products.Add(new Product { Id = 2, CreationDate = DateTime.Now.AddDays(-50), Description = "Product description 2", Name = "Product2", Price = 20.20M });
            context.Products.Add(new Product { Id = 3, CreationDate = DateTime.Now.AddYears(-1), Description = "Product description 3", Name = "Product3", Price = 30.20M });

            context.ProductOrders.Add(new ProductOrder { OrderId = 1, ProductId = 1, Quantity = 3, UnityPrice = 10.20M });
            context.ProductOrders.Add(new ProductOrder { OrderId = 1, ProductId = 2, Quantity = 1, UnityPrice = 20.20M });
            context.ProductOrders.Add(new ProductOrder { OrderId = 2, ProductId = 1, Quantity = 3, UnityPrice = 10.20M });
            context.ProductOrders.Add(new ProductOrder { OrderId = 2, ProductId = 2, Quantity = 2, UnityPrice = 20.20M });
            context.ProductOrders.Add(new ProductOrder { OrderId = 2, ProductId = 3, Quantity = 1, UnityPrice = 30.20M });

            context.SaveChanges();
        }

        public CommerceContext CommerceContext()
        {
            return context;
        }

        public void DropCommerceContext()
        {
            context.Database.EnsureDeleted();
        }
    }
}
