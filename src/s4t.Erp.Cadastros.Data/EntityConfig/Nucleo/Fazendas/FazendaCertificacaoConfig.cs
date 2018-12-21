using System.Data.Entity.ModelConfiguration;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;

namespace s4t.Erp.Cadastros.Data.EntityConfig.Nucleo.Fazendas
{
    public class FazendaCertificacaoConfig : EntityTypeConfiguration<FazendaCertificacao>
    {
        public FazendaCertificacaoConfig()
        {
            HasRequired(m => m.Fazenda)
                .WithMany(f => f.Certificacoes)
                .HasForeignKey(x => x.FazendaId);
        }
    }
}