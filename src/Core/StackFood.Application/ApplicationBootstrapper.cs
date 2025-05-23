using Microsoft.Extensions.DependencyInjection;
using StackFood.Application.UseCases.Orders.Create;
using StackFood.Application.UseCases.Orders.GetAll;
using StackFood.Application.UseCases.Orders.GetById;

namespace StackFood.Application
{
    public static class ApplicationBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
            services.AddScoped<IGetAllOrderUseCase, GetAllOrderUseCase>();
            services.AddScoped<IGetByIdOrderUseCase, GetByIdOrderUseCase>();
        }
    }
}
