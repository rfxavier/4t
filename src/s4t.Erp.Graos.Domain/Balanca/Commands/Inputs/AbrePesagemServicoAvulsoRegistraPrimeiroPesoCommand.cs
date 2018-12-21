using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Inputs
{
    public class AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand : ICommand
    {
        public AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand(Guid ticketPortariaId, double peso)
        {
            TicketPortariaId = ticketPortariaId;
            Peso = peso;
        }

        public Guid TicketPortariaId { get; private set; }
        public Double Peso { get; private set; }
    }
}