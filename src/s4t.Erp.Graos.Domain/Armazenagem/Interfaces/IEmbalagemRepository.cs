using System.Collections.Generic;
using s4t.Erp.Graos.Domain.Nucleo.Entities;

namespace s4t.Erp.Graos.Domain.Armazenagem.Interfaces
{
    public interface IEmbalagemRepository
    {
        IEnumerable<Embalagem> ObterPorListaDeIds(string ids);
    }
}