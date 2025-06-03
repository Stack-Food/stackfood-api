namespace StackFood.API.Requests.Products
{
    public record UpdateProductRequest(Guid Id, string Name, string Desc, decimal Price, string Img, int Category);
}
