using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Fiscal.Data.Context;
using System;
using System.Data.Entity;

namespace s4t.Erp.Fiscal.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected FiscalContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(FiscalContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}