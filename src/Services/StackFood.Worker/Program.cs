using MercadoPago.Config;
using Microsoft.EntityFrameworkCore;
using StackFood.Application;
using StackFood.Infra;
using StackFood.Infra.ExternalService.MercadoPago;
using StackFood.Infra.Persistence;

MercadoPagoConfig.AccessToken = "APP_USR-3012794291586434-051711-5fe595076a0027ab8a6be1bde5cd28f7-709468526";

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
        services.AddSingleton<IConfiguration>(context.Configuration);

        MercadoPagoBootstrapper.Register(services);
        InfraBootstrapper.Register(services);
        ApplicationBootstrapper.Register(services);

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();