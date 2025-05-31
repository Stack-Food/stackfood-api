using Microsoft.AspNetCore.Mvc;

namespace StackFood.API.Requests.Customers
{
    public class GetCustomerByCpfRequest
    {
        [FromRoute(Name = "cpf")]
        public required string Cpf { get; init; }
    }
}
