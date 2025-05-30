using Microsoft.Extensions.DependencyInjection;
using StackFood.Application.Interfaces.Services;
using StackFood.Application.Services;
using StackFood.Application.UseCases.Customers.Create;
using StackFood.Application.UseCases.Customers.GetByCpf;
using StackFood.Application.UseCases.Orders.ChangeStatus;
using StackFood.Application.UseCases.Orders.Create;
using StackFood.Application.UseCases.Orders.GetAll;
using StackFood.Application.UseCases.Orders.GetById;
using StackFood.Application.UseCases.Orders.Payments.Check;
using StackFood.Application.UseCases.Orders.Payments.Generate;

namespace StackFood.Application
{
    public static class ApplicationBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
            services.AddScoped<IGetAllOrderUseCase, GetAllOrderUseCase>();
            services.AddScoped<IGetByIdOrderUseCase, GetByIdOrderUseCase>();
            services.AddScoped<IGeneratePaymentUseCase, GeneratePaymentUseCase>();
            services.AddScoped<ICheckPaymentUseCase, CheckPaymentUseCase>();
            services.AddScoped<IChangeStatusOrderUseCase, ChangeStatusOrderUseCase>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
            services.AddScoped<IGetByCpfCustomerUseCase, GetByCpfCustomerUseCase>();
        }
    }
}
