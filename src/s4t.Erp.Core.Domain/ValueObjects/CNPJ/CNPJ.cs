using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.ValueObjects
{
    public class CNPJ: ValueObject<CNPJ>
    {
        public string Numero { get; private set; }

        protected CNPJ()
        {

        }

        public CNPJ(string numero)
        {
            Numero = numero;
        }
    }
}