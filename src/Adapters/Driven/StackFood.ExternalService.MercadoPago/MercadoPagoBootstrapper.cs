using Microsoft.Extensions.DependencyInjection;
using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.ExternalService.MercadoPago.Service;

namespace StackFood.ExternalService.MercadoPago
{
    public static class MercadoPagoBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMercadoPagoApiService, MercadoPagoApiService>();
        }
    }
}