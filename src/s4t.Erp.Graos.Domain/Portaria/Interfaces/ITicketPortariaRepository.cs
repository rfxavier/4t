using System;
using s4t.Erp.Graos.Domain.Portaria.Entities;

namespace s4t.Erp.Graos.Domain.Portaria.Interfaces
{
    public interface ITicketPortariaRepository
    {
        TicketPortaria GetById(Guid id);
        TicketPortaria Add(TicketPortaria ticketPortaria);
    }
}
