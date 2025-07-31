using StackFood.Application.Interfaces.Services;
using StackFood.Application.UseCases.Orders.Payments.Check;
using StackFood.Domain.Enums;

public class Worker(
    ILogger<Worker> logger,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var checkPaymentUseCase = scope.ServiceProvider.GetRequiredService<ICheckPaymentUseCase>();
                await checkPaymentUseCase.CheckPaymentAsync();
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}