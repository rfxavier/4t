using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Nucleo.Entities
{
    public class LoteEmbalagemNumeracao : Entity
    {
        public LoteEmbalagemNumeracao(Guid id, string numero)
        {
            Id = id;
            Numero = numero;
        }

        public string Numero { get; private set; }
    }
}
