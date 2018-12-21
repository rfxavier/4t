using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Cadastros.Data.EntityConfig.Nucleo.Nucleo
{
    public class CadastroConfig : EntityTypeConfiguration<Cadastro>
    {
        public CadastroConfig()
        {
            Property(c => c.TipoPfpj.Value)
                .HasColumnName("TipoPfPj");

            HasOptional(c => c.Usuario)
                .WithRequired(u => u.Cadastro);
        }
    }
}
