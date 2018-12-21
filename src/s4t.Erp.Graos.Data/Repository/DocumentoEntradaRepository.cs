using s4t.Erp.Graos.Data.Context;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace s4t.Erp.Graos.Data.Repository
{
    public class DocumentoEntradaRepository : Repository<DocumentoEntrada>, IDocumentoEntradaRepository
    {
        public DocumentoEntradaRepository(GraosContext context) : base(context)
        {
        }

        public DocumentoEntrada ObterPorId(Guid id)
        {
            return DbSet.Find(id);
        }

        public DocumentoEntrada Add(DocumentoEntrada documentoEntrada)
        {
            return DbSet.Add(documentoEntrada);
        }

        public DocumentoEntrada Update(DocumentoEntrada documentoEntrada)
        {
            var entry = Db.Entry(documentoEntrada);
            DbSet.Attach(documentoEntrada);
            entry.State = EntityState.Modified;

            return documentoEntrada;
        }

        public DocumentoEntrada ObterPorNumero(Guid filialId, int numero)
        {
            return DbSet.FirstOrDefault(d => d.FilialId == filialId && d.Numero == numero);
        }

        public int ObterProximaNumeracao(Guid filialId)
        {
            return 1;
        }

    }
}