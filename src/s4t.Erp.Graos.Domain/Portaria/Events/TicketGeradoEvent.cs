using s4t.Erp.Core.Domain.DomainNotification.Events.Contracts;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Events
{
    public class TicketGeradoEvent : IDomainEvent
    {
        public TicketGeradoEvent()
        {
        }

        public DateTime Date { get; }
    }
}