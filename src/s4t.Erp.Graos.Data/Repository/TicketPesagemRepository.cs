using s4t.Erp.Graos.Data.Context;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Balanca.Interfaces;
using System;
using System.Data.Entity;

namespace s4t.Erp.Graos.Data.Repository
{
    public class TicketPesagemRepository : Repository<TicketPesagem>, ITicketPesagemRepository
    {
        public TicketPesagemRepository(GraosContext context) : base(context)
        {
        }

        public TicketPesagem GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public TicketPesagem Add(TicketPesagem ticketPesagem)
        {
            return DbSet.Add(ticketPesagem);
        }

        public TicketPesagem Update(TicketPesagem ticketPesagem)
        {
            var entry = Db.Entry(ticketPesagem);
            DbSet.Attach(ticketPesagem);
            entry.State = EntityState.Modified;

            return ticketPesagem;
        }

        public int GetProximaNumeracao(Guid filialId)
        {
            return 1;
        }
    }
}