using System;

namespace s4t.Erp.Core.Domain.DomainNotification.Events.Contracts
{
    public interface IDomainEvent
    {
        DateTime Date { get; }
    }
}