using System;
using s4t.Erp.Core.Domain.Commands;

namespace s4t.Erp.Fiscal.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
        void Rollback();
    }
}
