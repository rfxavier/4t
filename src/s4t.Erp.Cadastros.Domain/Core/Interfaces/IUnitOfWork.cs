using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Cadastros.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
        void Rollback();
    }
}
