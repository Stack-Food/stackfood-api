using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackFood.Application.Interfaces.Services;
using StackFood.Infra.Services;
using Microsoft.EntityFrameworkCore;
using StackFood.Infra.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IOrderPaymentService, OrderPaymentService>();
        services.AddScoped<IExternalPaymentGateway, MercadoPagoGateway>();
        services.AddSingleton<IConfiguration>(context.Configuration);
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();