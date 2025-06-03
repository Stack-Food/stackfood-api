namespace StackFood.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        protected Customer() { }

        public Customer(string name, string email, string cpf)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}
