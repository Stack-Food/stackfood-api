using Microsoft.Extensions.DependencyInjection;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Infra.Persistence.Repositories;

namespace StackFood.Infra
{
    public static class InfraBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
