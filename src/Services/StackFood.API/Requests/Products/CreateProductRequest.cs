namespace StackFood.API.Requests.Products
{
    public record CreateProductRequest(string Name, string Desc, decimal Price, string Img, int Category);
}
