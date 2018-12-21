using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Nucleo
{
    public class LoteConfig : EntityTypeConfiguration<Lote>
    {
        public LoteConfig()
        {
            HasOptional(l => l.TicketPesagemMovimentacao)
                .WithOptionalPrincipal()
                .Map(x => x.MapKey("LoteId"));
        }
    }
}