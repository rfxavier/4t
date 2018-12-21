using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario ObterPorId(Guid id);

    }
}