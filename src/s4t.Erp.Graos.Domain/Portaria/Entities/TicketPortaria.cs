using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Portaria.Enums;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Entities
{
    public class TicketPortaria: Entity
    {
        //todo testes construtores
        public TicketPortaria(Guid id, Guid filialId, string numero, RegistroDePortaria registroDePortaria)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            TipoServicoPortaria = TipoServicoPortaria.Normal;
            RegistroDePortaria = registroDePortaria;
        }

        public TicketPortaria(Guid id, Guid filialId, string numero, RegistroDePortariaServicoAvulso registroDePortariaServicoAvulso)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            TipoServicoPortaria = TipoServicoPortaria.Avulso;
            RegistroDePortariaServicoAvulso = registroDePortariaServicoAvulso;
        }

        public Guid FilialId { get; private set; }
        public string Numero { get; private set; }
        public TipoServicoPortaria TipoServicoPortaria { get; private set; }
        public RegistroDePortaria RegistroDePortaria { get; private set; }
        public RegistroDePortariaServicoAvulso RegistroDePortariaServicoAvulso { get; private set; }
    }
}
