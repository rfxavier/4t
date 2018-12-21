using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Inputs
{
    public class ContinuaPesagemRegistraPesoCommand : ICommand
    {
        public ContinuaPesagemRegistraPesoCommand(Guid ticketPesagemId, double peso)
        {
            TicketPesagemId = ticketPesagemId;
            Peso = peso;
        }

        public Guid TicketPesagemId { get; private set; }
        public double Peso { get; private set; }
    }
}