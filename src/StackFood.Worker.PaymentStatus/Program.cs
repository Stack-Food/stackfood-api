using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackFood.Application.Interfaces.Services;
using StackFood.Infra.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(...); // configure sua conexão
        services.AddScoped<IOrderPaymentService, OrderPaymentService>();
        services.AddScoped<IExternalPaymentGateway, MercadoPagoGateway>();
        services.AddSingleton<IConfiguration>(context.Configuration); // Adicione esta linha
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();