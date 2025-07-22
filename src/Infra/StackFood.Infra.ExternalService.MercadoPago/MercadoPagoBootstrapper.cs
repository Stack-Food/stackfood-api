using Microsoft.Extensions.DependencyInjection;
using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.Infra.ExternalService.MercadoPago.Service;

namespace StackFood.Infra.ExternalService.MercadoPago
{
    public static class MercadoPagoBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMercadoPagoApiService, MercadoPagoApiService>();
        }
    }
}