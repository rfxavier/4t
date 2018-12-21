using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Cadastros.Data.EntityConfig.Nucleo.Nucleo
{
    public class FazendaConfig : EntityTypeConfiguration<Fazenda>
    {
        public FazendaConfig()
        {
            Property(a => a.FazendaStatus.Value)
                .HasColumnType("int");
        }
    }
}
