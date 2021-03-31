using Commerce.Domain.DTOs;
using Commerce.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Create(AddProductRequest request)
        {
            await productService.AddAsync(request);

            return Ok();
        }
    }
}
