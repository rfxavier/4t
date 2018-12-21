using s4t.Erp.Cadastros.Domain.Identity;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Cadastros.Data.EntityConfig.Identity
{
    public class UsuarioAspNetIdentityConfig : EntityTypeConfiguration<UsuarioAspNetIdentity>
    {
        public UsuarioAspNetIdentityConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            ToTable("AspNetUsers");
        }
    }
}