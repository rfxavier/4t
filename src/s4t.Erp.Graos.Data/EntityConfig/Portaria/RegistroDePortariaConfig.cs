using s4t.Erp.Graos.Domain.Portaria.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Portaria
{
    public class RegistroDePortariaConfig : EntityTypeConfiguration<RegistroDePortaria>
    {
        public RegistroDePortariaConfig()
        {
            Property(x => x.TipoGrao.Value)
                .HasColumnType("int");
        }
    }
}
