using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders
{
    public class UsuarioBuilder
    {
        private Guid _usuarioId = Guid.Empty;

        public Usuario Build()
        {
            return new Usuario(_usuarioId, string.Empty, string.Empty, null, Guid.Empty, false);
        }

        public UsuarioBuilder ComUsuarioId(Guid usuarioId)
        {
            this._usuarioId = usuarioId;
            return this;
        }

        public static implicit operator Usuario(UsuarioBuilder instance)
        {
            return instance.Build();
        }
    }
}