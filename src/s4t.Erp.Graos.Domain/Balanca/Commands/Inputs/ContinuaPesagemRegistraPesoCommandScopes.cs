using System;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Balanca.Entities;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Inputs
{
    public static class ContinuaPesagemRegistraPesoCommandScopes
    {
        public static bool PossuiPesoInvalido(this ContinuaPesagemRegistraPesoCommand continuaPesagemRegistraPesoCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertIsGreaterThan(Convert.ToDecimal(continuaPesagemRegistraPesoCommand.Peso), 0, "Peso inválido")
            );
        }

        public static bool PossuiTicketPesagemInformado(this ContinuaPesagemRegistraPesoCommand continuaPesagemRegistraPesoCommand,
            TicketPesagem ticketPesagem)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(ticketPesagem, "Ticket de pesagem não informado")
            );
        }

    }
}