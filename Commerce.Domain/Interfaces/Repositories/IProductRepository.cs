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
        /// <summary>
        /// Get a list of products based on multiple ids
        /// </summary>
        /// <param name="productIds">
        /// List of product ids
        /// </param>
        /// <returns>List of products</returns>
        public IEnumerable<Product> GetProducts(IEnumerable<int> productIds);
    }
    
}
