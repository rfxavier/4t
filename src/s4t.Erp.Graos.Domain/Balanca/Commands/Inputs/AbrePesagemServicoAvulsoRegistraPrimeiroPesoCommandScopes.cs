using System;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Portaria.Entities;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Inputs
{
    public static class AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandScopes
    {
        public static bool PossuiPesoInvalido(this AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand abrePesagemServicoAvulsoRegistraPrimeiroPesoCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertIsGreaterThan(Convert.ToDecimal(abrePesagemServicoAvulsoRegistraPrimeiroPesoCommand.Peso), 0, "Peso inválido")
            );
        }

        public static bool PossuiTicketPortariaInformado(this AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand abrePesagemRegistraPrimeiroPesoCommand,
            TicketPortaria ticketPortaria)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(ticketPortaria, "Ticket de portaria não informado")
            );
        }

    }
}