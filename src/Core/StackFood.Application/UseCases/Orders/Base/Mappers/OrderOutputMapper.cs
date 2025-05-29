using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Orders.Base.Mappers
{
    public static class OrderOutputMapper
    {
        public static OrderOutput Map(Order order)
        {
            return new OrderOutput
            {
                Id = order.Id,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                Products = order.Products.Select(po => new OrderProductOutput
                {
                    ProductId = po.ProductId,
                    Name = po.Name,
                    Description = po.Description,
                    Price = po.UnitPrice,
                    Quantity = po.Quantity,
                    ImageUrl = po.ImageUrl,
                    Category = po.Category
                }).ToList(),
                Customer = order.Customer is not null ? new OrderCustomerOutput
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Email = order.Customer.Email,
                    Cpf = order.Customer.Cpf
                } : null,
                Payment = order.Payment is not null ? new OrderPaymentOutput
                {
                    Id = order.Payment.Id,
                    PaymentExternalId = order.Payment.PaymentExternalId,
                    QrCodeUrl = order.Payment.QrCodeUrl,
                    Status = order.Payment.Status,
                    PaymentDate = order.Payment.PaymentDate,
                    Type = order.Payment.Type
                } : null
            };
        }

        public static IEnumerable<OrderOutput> Map(List<Order> orders)
        {
            return orders.Select(Map);
        }
    }
}
