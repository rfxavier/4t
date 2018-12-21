using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.DomainNotification.Events.Contracts;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents
{
    public class RegistroDePortariaSendoCriadoEvent : IDomainEvent
    {
        public RegistroDePortariaSendoCriadoEvent(RegistroDePortaria registroDePortaria, Filial filial)
        {
            RegistroDePortaria = registroDePortaria;
            Filial = filial;
            Date = DateTime.Now;
        }

        public RegistroDePortaria RegistroDePortaria { get; private set; }
        public Filial Filial { get; private set; }
        public DateTime Date { get; }
    }
}
