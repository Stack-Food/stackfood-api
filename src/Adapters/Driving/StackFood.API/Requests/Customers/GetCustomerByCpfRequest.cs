using Microsoft.AspNetCore.Mvc;

namespace StackFood.API.Requests.Customers
{
    public record GetCustomerByCpfRequest(string Cpf);
}
