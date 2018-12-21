using System;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;

namespace s4t.Erp.Graos.Domain.Armazenagem.Interfaces
{
    public interface IArmazemRepository
    {
        Armazem ObterPorId(Guid id);
        Armazem ObterPorCodigo(Guid filialId, string codigo);
    }
}