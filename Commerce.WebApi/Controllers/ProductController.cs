using Commerce.Domain.DTOs;
using Commerce.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Create a product.
        /// </summary>
        /// <param name="request">Product details</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] AddProductRequest request)
        {
            await productService.AddAsync(request);

            return Ok();
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="request">Product details</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateProductRequest request)
        {
            await productService.UpdateAsync(id, request);

            return Ok();
        }
        /// <summary>
        /// Get products based on filters passed
        /// </summary>
        /// <param name="Name">Product name</param>
        /// <param name="Description">Product description</param> 
        /// <param name="Price">Product's price</param>
        /// <param name="StartDate">Start date for the period the product was created</param> 
        /// <param name="EndDate">End date for the period the product was created</param>
        /// <param name="OrderBy">Sorting criteria</param> 
        /// <response code="200">Products list</response>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] GetProductsRequest dto)
        {
            return Ok(productService.GetProducts(dto));
        }
    }
}
