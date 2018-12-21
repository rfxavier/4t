using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs;
using System;

namespace s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Commands.Builders
{
    public class AbreDocumentoEntradaCommandBuilder
    {
        private Guid _filialId = Guid.Empty;
        private Guid _notaFiscalGraosId = Guid.Empty;
        private int _tipoOperacao;

        public AbreDocumentoEntradaCommand Build()
        {
            return new AbreDocumentoEntradaCommand(_filialId, _notaFiscalGraosId, _tipoOperacao);
        }

        public AbreDocumentoEntradaCommandBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public AbreDocumentoEntradaCommandBuilder ComNotaFiscalGraosId(Guid notaFiscalGraosId)
        {
            this._notaFiscalGraosId = notaFiscalGraosId;
            return this;
        }

        public AbreDocumentoEntradaCommandBuilder ComTipoOperacao(int tipoOperacao)
        {
            this._tipoOperacao = tipoOperacao;
            return this;
        }


        public static implicit operator AbreDocumentoEntradaCommand(
            AbreDocumentoEntradaCommandBuilder instance)
        {
            return instance.Build();
        }
    }

}
