using Commerce.Domain.DTOs;
using Commerce.Domain.Interfaces.Services;
using Commerce.Services.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Services
{
	public static class StartupSetup
	{
		public static void AddServices(this IServiceCollection services) 
		{
			services.AddScoped<IUserServices, UserServices>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IOrderService, OrderService>();
		}

		public static void AddValidators(this IServiceCollection services)
		{
			services.AddScoped<IValidator<SignUpRequest>, SignUpValidator>();
		}
	}
}
