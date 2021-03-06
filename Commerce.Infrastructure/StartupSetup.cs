using Commerce.Domain.Interfaces.Clients;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Clients;
using Commerce.Infrastructure.Context;
using Commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Infrastructure
{
	public static class StartupSetup
	{
		public static void AddDbContext(this IServiceCollection services, string connectionString) =>
			services.AddDbContext<CommerceContext>(options =>
				options.UseLazyLoadingProxies()
					.UseSqlServer(connectionString));

		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
		}

		public static void AddClients(this IServiceCollection services)
		{
			services.AddScoped<IAuth0Client, Auth0Client>();
		}
	}
}
