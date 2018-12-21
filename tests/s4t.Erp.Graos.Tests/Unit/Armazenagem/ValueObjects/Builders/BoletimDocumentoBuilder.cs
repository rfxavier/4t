using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects.Builders
{
    public class BoletimDocumentoBuilder
    {
        //public BoletimSerie Serie { get; private set; }
        //public Guid DocumentoEntradaId { get; private set; }
        //public DocumentoEntrada DocumentoEntrada { get; private set; }
        //public Guid InstrucaoServicoId { get; private set; }
        //public InstrucaoServico InstrucaoServico { get; private set; }
        //public Guid TransferenciaId { get; private set; }
        //public Transferencia Transferencia { get; private set; }
        //public Guid OrdemCarregamentoId { get; private set; }
        //public OrdemCarregamento OrdemCarregamento { get; private set; }
        //public int RemocaoNumero { get; private set; }

        private BoletimSerie _boletimSerie = null;
        private DocumentoEntrada _documentoEntrada = null;
        private InstrucaoServico _instrucaoServico = null;
        private Transferencia _transferencia = null;
        private OrdemCarregamento _ordemCarregamento = null;
        private int _remocaoNumero = 0;

        public BoletimDocumento Build()
        {
            return new BoletimDocumento(_boletimSerie, _documentoEntrada, _instrucaoServico, _transferencia, _ordemCarregamento, _remocaoNumero);
        }

        public BoletimDocumentoBuilder ComSerie(BoletimSerie boletimSerie)
        {
            this._boletimSerie = boletimSerie;
            return this;
        }

        public BoletimDocumentoBuilder ComDocumentoEntrada(DocumentoEntrada documentoEntrada)
        {
            this._documentoEntrada = documentoEntrada;
            return this;
        }

        public BoletimDocumentoBuilder ComInstrucaoServico(InstrucaoServico instrucaoServico)
        {
            this._instrucaoServico = instrucaoServico;
            return this;
        }

        public BoletimDocumentoBuilder ComTransferencia(Transferencia transferencia)
        {
            this._transferencia = transferencia;
            return this;
        }

        public BoletimDocumentoBuilder ComOrdemCarregamento(OrdemCarregamento ordemCarregamento)
        {
            this._ordemCarregamento = ordemCarregamento;
            return this;
        }

        public BoletimDocumentoBuilder ComRemocaoNumero(int remocaoNumero)
        {
            this._remocaoNumero = remocaoNumero;
            return this;
        }

        public static implicit operator BoletimDocumento(BoletimDocumentoBuilder instance)
        {
            return instance.Build();
        }

    }
}