using s4t.Erp.Graos.Domain.Balanca.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Balanca
{
    public class TicketPesagemConfig : EntityTypeConfiguration<TicketPesagem>
    {
        public TicketPesagemConfig()
        {
            HasOptional(t => t.DocumentoEntrada)
                .WithOptionalPrincipal()
                .Map(x => x.MapKey("TicketPesagemId"));
        }
    }
}