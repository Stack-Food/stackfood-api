namespace StackFood.Application.UseCases.Orders.Base.Outputs
{
    public class OrderCustomerOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
    }
}