using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders
{
    public class LocalBuilder
    {
        private Guid _localId = Guid.Empty;
        private Guid _empresaId = Guid.Empty;
        private string _codigo;

        public Local Build()
        {
            return new Local(_localId, _empresaId, _codigo, string.Empty);
        }

        public LocalBuilder ComLocalId(Guid localId)
        {
            this._localId = localId;
            return this;
        }

        public LocalBuilder ComEmpresaId(Guid empresaId)
        {
            this._empresaId = empresaId;
            return this;
        }

        public LocalBuilder ComCodigo(string codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public static implicit operator Local(LocalBuilder instance)
        {
            return instance.Build();
        }

    }
}