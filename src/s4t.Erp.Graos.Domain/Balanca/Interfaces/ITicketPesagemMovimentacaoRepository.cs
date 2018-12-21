using System.Collections.Generic;
using s4t.Erp.Graos.Domain.Balanca.Entities;

namespace s4t.Erp.Graos.Domain.Balanca.Interfaces
{
    public interface ITicketPesagemMovimentacaoRepository
    {
        IEnumerable<TicketPesagemMovimentacao> ObterPorListaDeIds(string ids);
    }
}