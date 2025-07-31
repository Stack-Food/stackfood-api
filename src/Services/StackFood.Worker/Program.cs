using MercadoPago.Config;
using Microsoft.EntityFrameworkCore;
using StackFood.Application;
using StackFood.Infra;
using StackFood.Infra.ExternalService.MercadoPago;
using StackFood.Infra.Persistence;

MercadoPagoConfig.AccessToken = "TEST-3012794291586434-051711-8a400aab6fd7b21510de463589bbd8a1-709468526";


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