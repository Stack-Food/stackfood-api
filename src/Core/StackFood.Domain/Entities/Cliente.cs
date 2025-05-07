namespace StackFood.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        protected Cliente() { }

        public Cliente(string nome, string email, string cpf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }
    }
}
