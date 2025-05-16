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
                Products = order.Products.Select(po => new OrderProductOutput
                {
                    ProductId = po.Product.Id,
                    Name = po.Product.Name,
                    Description = po.Product.Description,
                    Price = po.Product.Price,
                    Quantity = po.Quantity,
                    ImageUrl = po.Product.ImageUrl,
                    Category = po.Product.Category
                }).ToList()
            };
        }

        public static IEnumerable<OrderOutput> Map(List<Order> orders)
        {
            return orders.Select(Map);
        }
    }
}
