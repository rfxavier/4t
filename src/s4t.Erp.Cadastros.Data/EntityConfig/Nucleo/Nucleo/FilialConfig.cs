using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Cadastros.Data.EntityConfig.Nucleo.Nucleo
{
    public class FilialConfig : EntityTypeConfiguration<Filial>
    {
        public FilialConfig()
        {
            HasRequired(f => f.Empresa)
                .WithMany(e => e.Filiais)
                .HasForeignKey(ffk => ffk.EmpresaId);
        }
    }
}