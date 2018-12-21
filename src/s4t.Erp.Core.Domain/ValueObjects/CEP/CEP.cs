using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.ValueObjects
{
    public class CEP : ValueObject<CEP>
    {
        public CEP(string numero)
        {
            Numero = numero;
        }

        protected CEP() { }

        public string Numero { get; protected set; }
    }
}