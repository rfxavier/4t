using s4t.Erp.Core.Domain.DomainNotification.Events.Contracts;

namespace s4t.Erp.Core.Domain.DomainNotification
{
    public interface IHandler<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}