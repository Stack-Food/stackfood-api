using Microsoft.Extensions.DependencyInjection;
using StackFood.Application.UseCases.Customers.Create;
using StackFood.Application.UseCases.Customers.GetByCpf;
using StackFood.Application.UseCases.Orders.ChangeStatus;
using StackFood.Application.UseCases.Orders.Create;
using StackFood.Application.UseCases.Orders.GetAll;
using StackFood.Application.UseCases.Orders.GetById;
using StackFood.Application.UseCases.Orders.Payments.Check;
using StackFood.Application.UseCases.Orders.Payments.Generate;
using StackFood.Application.UseCases.Products.Create;
using StackFood.Application.UseCases.Products.Delete;
using StackFood.Application.UseCases.Products.GetAll;
using StackFood.Application.UseCases.Products.GetById;
using StackFood.Application.UseCases.Products.Update;

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

            services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
            services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
            services.AddScoped<IGetAllProductUseCase, GetAllProductUseCase>();
            services.AddScoped<IGetByIdProductUseCase, GetByIdProductUseCase>();
            services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
            
            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
            services.AddScoped<IGetByCpfCustomerUseCase, GetByCpfCustomerUseCase>();
        }
    }
}
