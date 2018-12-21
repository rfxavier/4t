using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.RecepcaoExpedicao
{
    public class DocumentoEntradaConfig :  EntityTypeConfiguration<DocumentoEntrada>
    {
        public DocumentoEntradaConfig()
        {
            HasKey(n => n.Id);
            HasOptional(d => d.TicketPesagem)
                .WithOptionalPrincipal()
                .Map(x => x.MapKey("DocumentoEntradaId"));
        }
    }
}