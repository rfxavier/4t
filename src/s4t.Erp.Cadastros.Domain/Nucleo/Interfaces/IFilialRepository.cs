using System;
using s4t.Erp.Cadastros.Domain.Nucleo.Entities;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Interfaces
{
    public interface IFilialRepository
    {
        Filial ObterPorId(Guid id);
        Filial ObterPorCodigo(Guid empresaId, int codigo);
    }
}