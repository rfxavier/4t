using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Graos.Data.Context;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using System;

namespace s4t.Erp.Graos.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GraosContext _context;
        private bool _disposed;

        public UnitOfWork(GraosContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        CommandResponse IUnitOfWork.Commit()
        {
            _context.SaveChanges();
            return CommandResponse.Ok;
        }

        public void Rollback()
        {
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}