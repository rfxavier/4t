using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.DomainNotification.Events.Contracts;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents
{
    public class RegistroDePortariaServicoAvulsoSendoCriadoEvent : IDomainEvent
    {
        public RegistroDePortariaServicoAvulsoSendoCriadoEvent(RegistroDePortariaServicoAvulso registroDePortariaServicoAvulso, Filial filial)
        {
            RegistroDePortariaServicoAvulso = registroDePortariaServicoAvulso;
            Filial = filial;
            Date = DateTime.Now;
        }

        public RegistroDePortariaServicoAvulso RegistroDePortariaServicoAvulso { get; private set; }
        public Filial Filial { get; private set; }
        public DateTime Date { get; }
    }
}
