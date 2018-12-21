using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Results
{
    public class AbreDocumentoEntradaCommandResult : ICommandResult
    {
        public AbreDocumentoEntradaCommandResult()
        {
        }

        public AbreDocumentoEntradaCommandResult(Guid documentoEntradaId, string documentoEntradaNumero)
        {
            DocumentoEntradaId = documentoEntradaId;
            DocumentoEntradaNumero = documentoEntradaNumero;
        }

        public Guid DocumentoEntradaId { get; set; }
        public string DocumentoEntradaNumero { get; set; }
    }
}