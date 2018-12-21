using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.ValueObjects.Placa
{
    public class Placa: ValueObject<Placa>
    {
        public string Numero { get; protected set; }

        protected Placa()
        {
            
        }

        public Placa(string numero)
        {
            Numero = numero;
        }
    }
}