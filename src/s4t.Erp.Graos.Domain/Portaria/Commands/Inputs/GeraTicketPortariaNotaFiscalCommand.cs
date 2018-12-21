using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Inputs
{
    public class GeraTicketPortariaNotaFiscalCommand : ICommand
    {
        public GeraTicketPortariaNotaFiscalCommand(Guid notaFiscalId)
        {
            NotaFiscalId = notaFiscalId;
        }

        public Guid NotaFiscalId { get; set; }
    }
}
