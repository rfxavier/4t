using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders
{
    public class ArmazemBuilder
    {
        private Guid _armazemId = Guid.Empty;
        private Guid _filialId = Guid.Empty;
        private string _codigo;

        public Armazem Build()
        {
            return new Armazem(_armazemId, _filialId, _codigo, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, null, Guid.Empty, 0, 0, string.Empty);
        }

        public ArmazemBuilder ComArmazemId(Guid armazemId)
        {
            this._armazemId = armazemId;
            return this;
        }

        public ArmazemBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public ArmazemBuilder ComCodigo(string codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public static implicit operator Armazem(ArmazemBuilder instance)
        {
            return instance.Build();
        }
    }
}