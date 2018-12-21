using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.ValueObjects
{
    public class Telefone : ValueObject<Telefone>
    {
        public Telefone(string numero)
        {
            Numero = numero;
        }

        protected Telefone() { }
        public string Numero { get; protected set; }
    }
}