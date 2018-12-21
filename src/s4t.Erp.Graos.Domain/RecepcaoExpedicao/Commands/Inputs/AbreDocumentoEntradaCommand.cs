using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs
{
    public class AbreDocumentoEntradaCommand : ICommand
    {
        public AbreDocumentoEntradaCommand(Guid filialId, Guid notaFiscalGraosId, int tipoOperacao)
        {
            FilialId = filialId;
            NotaFiscalGraosId = notaFiscalGraosId;
            TipoOperacao = tipoOperacao;
            Data = DateTime.Now;
        }

        public Guid FilialId { get; private set; }
        public Guid NotaFiscalGraosId { get; private set; }
        public int TipoOperacao { get; private set; }
        public DateTime Data { get; private set; }
    }
}
