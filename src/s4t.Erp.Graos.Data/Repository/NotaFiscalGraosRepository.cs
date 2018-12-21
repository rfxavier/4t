using s4t.Erp.Graos.Data.Context;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Interfaces;
using System;

namespace s4t.Erp.Graos.Data.Repository
{
    public class NotaFiscalGraosRepository : Repository<NotaFiscalGraos>, INotaFiscalGraosRepository
    {
        public NotaFiscalGraosRepository(GraosContext context) : base(context)
        {
        }

        public NotaFiscalGraos GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public NotaFiscalGraos Add(NotaFiscalGraos notaFiscalGraos)
        {
            return DbSet.Add(notaFiscalGraos);
        }
    }
}