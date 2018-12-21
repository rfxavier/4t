using s4t.Erp.Graos.Domain.Balanca.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Balanca
{
    public class TicketPesagemMovimentacaoConfig : EntityTypeConfiguration<TicketPesagemMovimentacao>
    {
        public TicketPesagemMovimentacaoConfig()
        {
            HasRequired(m => m.TicketPesagem)
                .WithMany(t => t.TicketPesagemMovimentacoes)
                .HasForeignKey(t => t.TicketPesagemId);

            HasOptional(t => t.Lote)
                .WithOptionalPrincipal()
                .Map(x => x.MapKey("TicketPesagemMovimentacaoId"));

        }
    }
}