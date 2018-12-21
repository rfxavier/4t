using s4t.Erp.Core.Domain.Commands;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Results
{
    public class GeraTicketCommandResult : ICommandResult
    {
        public GeraTicketCommandResult()
        {
        }

        public GeraTicketCommandResult(string numeroTicket)
        {
            NumeroTicket = numeroTicket;
        }

        public string NumeroTicket { get; set; }
    }

}
