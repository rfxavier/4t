using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Armazenagem
{
    public class BoletimConfig : EntityTypeConfiguration<Boletim>
    {
        public BoletimConfig()
        {
            HasRequired(b => b.Lote)
                .WithMany(l => l.Boletins)
                .HasForeignKey(b => b.LoteId);
        }
    }
}