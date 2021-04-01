using Commerce.Domain.DTOs;
using Commerce.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="request">Order details</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody]AddOrderRequest request)
        {
            await orderService.AddOrderAsync(request);
            
            return Ok();
        }
        /// <summary>
        /// Search for orders with the possibility of filetering by the username of the user that made the order
        /// the product that might be in the order, order id or creation date, if no filters are passed returns all orders
        /// </summary>
        /// <param name="request">Data to filter the search</param>
        /// <returns>List of orders</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery]GetOrderRequest request)
        {
            var result = orderService.GetOrders(request);

            return Ok(result);
        }
    }
}
