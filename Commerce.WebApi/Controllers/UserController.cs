using Commerce.Domain.Interfaces.Services;
using Commerce.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userService;

        public UserController(IUserServices userService)
        {
            this.userService = userService;
        }
        /// <summary>
        /// Get users based on filters passed
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="EmailAddress">Email address</param> 
        /// <param name="DisplayName">Display name</param>
        /// <param name="StartDate">Start date for the period the user was created</param> 
        /// <param name="EndDate">End date for the period the user was created</param>
        /// <param name="OrderBy">Sorting criteria</param> 
        /// <response code="200">Users list</response>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery]GetUsersRequest dto)
        {
            return Ok(userService.GetUsers(dto));
        }
        /// <summary>
        /// Add an user if they don't already exist
        /// </summary>
        /// <param name="signupDto">User's data</param>
        /// <response code="200">User created</response>
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest signupDto)
        {
            await userService.SignUpAsync(signupDto);

            return Ok();
        }
        /// <summary>
        /// Sign the user in the service if they exist on the database
        /// </summary>
        /// <param name="dto">Username and password</param>
        /// <response code="200">User and token</response>
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest dto)
        {
            var user = await userService.SignInAsync(dto);

            return Ok(user);
        }
    }
}
