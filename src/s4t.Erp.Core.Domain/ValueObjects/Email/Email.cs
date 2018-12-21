using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public string Endereco { get; protected set; }

        protected Email() { }

        public Email(string endereco)
        {
            this.Endereco = endereco;
        }
    }
}
