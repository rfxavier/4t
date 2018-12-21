using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Nucleo.Entities
{
    public class NotaFiscalGraosCertificado : Entity
    {
        public NotaFiscalGraosCertificado()
        {
            Id = Guid.NewGuid();
        }
        public Guid CertificadoEmissorId { get; set; }
        public int CertificadoEmissorCodigo { get; set; }
        public string CertificadoEmissorNome { get; set; }
        public string CertificacaoNumero { get; set; }
    }
}