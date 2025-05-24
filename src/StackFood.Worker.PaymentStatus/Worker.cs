using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Enums;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IOrderPaymentService _orderPaymentService;
    private readonly IExternalPaymentGateway _paymentGateway;

    public Worker(
        ILogger<Worker> logger,
        IOrderPaymentService orderPaymentService,
        IExternalPaymentGateway paymentGateway)
    {
        _logger = logger;
        _orderPaymentService = orderPaymentService;
        _paymentGateway = paymentGateway;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var orders = await _orderPaymentService.GetPendingPaymentOrdersAsync();

            foreach (var order in orders)
            {
                var status = await _paymentGateway.GetPaymentStatusAsync(order);
                await _orderPaymentService.UpdateOrderPaymentStatusAsync(order, status);

                _logger.LogInformation($"Order {order.Id} updated to {status}");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}