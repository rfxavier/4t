using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.ValueObjects
{
    public class CPF: ValueObject<CPF>
    {
        public string Numero { get; private set; }

        protected CPF()
        {
            
        }

        public CPF(string numero)
        {
            Numero = numero;
        }
    }
}