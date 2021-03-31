using Commerce.Domain.DTOs;
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
    }
}
