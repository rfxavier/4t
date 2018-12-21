using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Nucleo
{
    public class FornecedorGraosConfig : EntityTypeConfiguration<FornecedorGraos>
    {
        public FornecedorGraosConfig()
        {
            ToTable("Cadastro");
        }
    }
}
