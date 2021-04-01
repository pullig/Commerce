using Commerce.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commerce.Domain.Interfaces.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="request">Product details</param>
        /// <returns></returns>
        public Task AddAsync(AddProductRequest request);
        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="request">Product details</param>
        /// <returns></returns>
        public Task UpdateAsync(int id, UpdateProductRequest request);
        /// <summary>
        /// Get products based on filters passed
        /// </summary>
        /// <param name="dto">
        /// Filters to search for a product, if no filters are passed will return all products
        /// </param>
        /// <returns>List of products</returns>
        public IEnumerable<GetProductsResult> GetProducts(GetProductsRequest dto);
    }
}
