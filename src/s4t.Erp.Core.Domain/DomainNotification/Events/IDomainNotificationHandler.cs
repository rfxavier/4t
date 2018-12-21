using System.Collections.Generic;
using s4t.Erp.Core.Domain.DomainNotification.Events.Contracts;

namespace s4t.Erp.Core.Domain.DomainNotification.Events
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T: IDomainEvent
    {
        List<T> Notify();
        bool HasNotifications();
    }
}
