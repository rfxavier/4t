using s4t.Erp.Graos.Domain.Balanca.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Balanca.Entities.Builders
{
    public class TicketPesagemMovimentacaoBuilder
    {
        private Guid _ticketPesagemMovimentacaoId = Guid.Empty;

        public TicketPesagemMovimentacao Build()
        {
            return new TicketPesagemMovimentacao(_ticketPesagemMovimentacaoId, 0);
        }

        public TicketPesagemMovimentacaoBuilder ComTicketPesagemMovimentacaoId(Guid ticketPesagemMovimentacaoId)
        {
            this._ticketPesagemMovimentacaoId = ticketPesagemMovimentacaoId;
            return this;
        }

        public static implicit operator TicketPesagemMovimentacao(TicketPesagemMovimentacaoBuilder instance)
        {
            return instance.Build();
        }


    }
}