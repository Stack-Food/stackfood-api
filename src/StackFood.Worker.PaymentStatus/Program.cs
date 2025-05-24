using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackFood.Application.Interfaces.Services;
using StackFood.Infra.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(...); // configure sua conexão
        services.AddScoped<IOrderPaymentService, OrderPaymentService>();
        services.AddScoped<IExternalPaymentGateway, MercadoPagoGateway>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();