using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Nucleo
{
    public class NotaFiscalGraosConfig : EntityTypeConfiguration<NotaFiscalGraos>
    {
        public NotaFiscalGraosConfig()
        {
            HasKey(n => n.Id);
            HasOptional(n => n.DocumentoEntrada)
                .WithRequired(d => d.NotaFiscalGraos);

            HasOptional(n => n.RegistroDePortaria)
                .WithMany(r => r.NotasFiscais)
                .HasForeignKey(n => n.RegistroDePortariaId);

            Property(n => n.NotaFiscalDataEmissao)
                .HasColumnType("datetime2");

            Property(n => n.NotaFiscalDataSaida)
                .HasColumnType("datetime2");
        }
    }
}