using System;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;

namespace s4t.Erp.Graos.Domain.Armazenagem.Interfaces
{
    public interface ILocalRepository
    {
        Local ObterPorId(Guid id);
        Local ObterPorCodigo(Guid filialId, string codigo);
        Local ObterPorLocalizacao(Localizacao localizacao);

    }
}