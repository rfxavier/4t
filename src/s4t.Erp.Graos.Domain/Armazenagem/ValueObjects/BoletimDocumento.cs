using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.ValueObjects
{
    public class BoletimDocumento : ValueObject<BoletimDocumento>
    {
        public BoletimSerie Serie { get; private set; }
        public Guid DocumentoEntradaId { get; private set; }
        public DocumentoEntrada DocumentoEntrada { get; private set; }
        public Guid InstrucaoServicoId { get; private set; }
        public InstrucaoServico InstrucaoServico { get; private set; }
        public Guid TransferenciaId { get; private set; }
        public Transferencia Transferencia { get; private set; }
        public Guid OrdemCarregamentoId { get; private set; }
        public OrdemCarregamento OrdemCarregamento { get; private set; }
        public int RemocaoNumero { get; private set; }

        protected BoletimDocumento()
        {
        }

        public BoletimDocumento(BoletimSerie serie, DocumentoEntrada documentoEntrada,
            InstrucaoServico instrucaoServico, Transferencia transferencia, OrdemCarregamento ordemCarregamento,
            int remocaoNumero)
        {
            Serie = serie;

            DocumentoEntradaId = documentoEntrada == null ? Guid.Empty : documentoEntrada.Id;
            DocumentoEntrada = documentoEntrada;
            InstrucaoServicoId = instrucaoServico == null ? Guid.Empty : instrucaoServico.Id;
            InstrucaoServico = instrucaoServico;
            TransferenciaId = transferencia == null ? Guid.Empty : transferencia.Id;
            Transferencia = transferencia;
            OrdemCarregamentoId = ordemCarregamento == null ? Guid.Empty : ordemCarregamento.Id;
            OrdemCarregamento = ordemCarregamento;

            RemocaoNumero = remocaoNumero;
        }
    }
}