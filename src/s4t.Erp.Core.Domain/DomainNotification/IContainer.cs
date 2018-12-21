using System;
using System.Collections.Generic;

namespace s4t.Erp.Core.Domain.DomainNotification
{
    public interface IContainer
    {
        T GetService<T>();
        object GetService(Type serviceType);
        IEnumerable<T> GetServices<T>();
        IEnumerable<object> GetServices(Type serviceType);
    }
}