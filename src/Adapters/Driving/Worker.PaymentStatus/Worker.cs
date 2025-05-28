using StackFood.Application.Interfaces.Services;
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
                var orderPaymentService = scope.ServiceProvider.GetRequiredService<IOrderPaymentService>();
                // var paymentGateway = scope.ServiceProvider.GetRequiredService<IExternalPaymentGateway>();

                var orders = await orderPaymentService.GetPendingPaymentOrdersAsync();

                foreach (var order in orders)
                {
                    // var status = await paymentGateway.GetPaymentStatusAsync(order);
                    await orderPaymentService.UpdateOrderPaymentStatusAsync(order, PaymentStatus.Paid);

                    _logger.LogInformation($"Order {order.Id} updated to approved");
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}