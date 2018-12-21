using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs
{
    public class ComplementaDocumentoEntradaComPesosLoteCommand : ICommand
    {
        public Guid LoteId { get; set; }
        public double Peso { get; set; }
        public Guid TicketPesagemMovimentacaoId { get; set; }
        public Guid EmbalagemId { get; set; }
        public int QuantidadeEmbalagem { get; set; }
    }
}