using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.RecepcaoExpedicao
{
    public class DocumentoEntradaEmbalagemConfig : EntityTypeConfiguration<DocumentoEntradaEmbalagem>
    {
        public DocumentoEntradaEmbalagemConfig()
        {
            HasRequired(d => d.DocumentoEntrada)
                .WithMany(de => de.Embalagens)
                .HasForeignKey(de => de.DocumentoEntradaId);
        }
    }
}