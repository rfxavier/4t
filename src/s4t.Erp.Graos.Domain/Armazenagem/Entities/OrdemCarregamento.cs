using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Entities
{
    public class OrdemCarregamento : Entity
    {
        public OrdemCarregamento(Guid id, Guid filialId, int numero, DateTime data)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            Data = data;
        }
        public Guid FilialId { get; private set; }
        public int Numero { get; private set; }
        public DateTime Data { get; private set; }
    }
}