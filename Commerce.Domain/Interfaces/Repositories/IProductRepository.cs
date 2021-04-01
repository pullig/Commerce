using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using System.Collections.Generic;

namespace Commerce.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Get products based on filters passed
        /// </summary>
        /// <param name="dto">
        /// Filters to search for a product, if no filters are passed will return all products
        /// </param>
        /// <returns>List of products</returns>
        public IEnumerable<Product> GetProducts(GetProductsRequest dto);
    }
    
}
