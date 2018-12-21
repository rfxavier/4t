using s4t.Erp.Fiscal.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Fiscal.Data.EntityConfig
{
    public class NotaFiscalConfig : EntityTypeConfiguration<NotaFiscal>
    {
        public NotaFiscalConfig()
        {
            //HasRequired(n => n.Emitente)
            //    .WithMany()
            //    .HasForeignKey(n => n.EmitenteId);

            //HasRequired(n => n.Destinatario)
            //    .WithMany()
            //    .HasForeignKey(n => n.DestinatarioId);

            //HasRequired(n => n.DestinatarioFazenda)
            //    .WithMany()
            //    .HasForeignKey(n => n.DestinatarioFazendaId);
        }
    }
}