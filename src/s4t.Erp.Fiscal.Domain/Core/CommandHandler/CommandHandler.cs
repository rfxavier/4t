using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Fiscal.Domain.Core.Interfaces;

namespace s4t.Erp.Fiscal.Domain.Core.CommandHandler
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public CommandHandler(IUnitOfWork uow, IDomainNotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = notifications;
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            return false;
        }
    }
}