using System;
using s4t.Erp.Graos.Domain.Balanca.Entities;

namespace s4t.Erp.Graos.Domain.Balanca.Interfaces
{
    public interface ITicketPesagemRepository
    {
        TicketPesagem GetById(Guid id);
        TicketPesagem Add(TicketPesagem ticketPesagem);
        TicketPesagem Update(TicketPesagem ticketPesagem);
        int GetProximaNumeracao(Guid filialId);

    }
}