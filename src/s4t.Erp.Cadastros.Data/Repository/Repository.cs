using s4t.Erp.Cadastros.Data.Context;
using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using System;
using System.Data.Entity;

namespace s4t.Erp.Cadastros.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected CadastrosContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(CadastrosContext context)
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