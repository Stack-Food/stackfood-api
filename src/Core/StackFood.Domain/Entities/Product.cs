using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; }
        public ProductCategory Category { get; private set; }

        protected Product() { }

        public Product(string name, string desc, decimal price, string imageUrl, ProductCategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = desc;
            Price = price;
            ImageUrl = imageUrl;
            Category = category;
        }
    }

}
