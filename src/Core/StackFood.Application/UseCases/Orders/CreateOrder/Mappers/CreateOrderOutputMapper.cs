using StackFood.Application.UseCases.Orders.CreateOrder.Outputs;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Orders.CreateOrder.Mappers
{
    public static class CreateOrderOutputMapper
    {
        public static CreateOrderOutput Map(Order order)
        {
            return new CreateOrderOutput
            {
                Id = order.Id,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Products = order.Products.Select(po => new CreateOrderProductOutput
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
    }
}
