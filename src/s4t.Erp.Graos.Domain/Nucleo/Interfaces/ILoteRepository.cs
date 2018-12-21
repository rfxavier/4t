using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Nucleo.Interfaces
{
    public interface ILoteRepository
    {
        Lote ObterPorNumero(Guid filialId, string numero);
    }
}