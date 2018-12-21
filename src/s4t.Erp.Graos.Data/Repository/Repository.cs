using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Data.Context;
using System;
using System.Data.Entity;

namespace s4t.Erp.Graos.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected GraosContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(GraosContext context)
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