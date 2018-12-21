using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders
{
    public class PilhaBuilder
    {
        private Guid _pilhaId = Guid.Empty;
        private Guid _filialId = Guid.Empty;
        private Guid _armazemId = Guid.Empty;

        public Pilha Build()
        {
            return new Pilha(_pilhaId, _filialId, _armazemId, string.Empty, string.Empty, false, 0, 0, 0);
        }

        public PilhaBuilder ComPilhaId(Guid pilhaId)
        {
            this._pilhaId = pilhaId;
            return this;
        }

        public PilhaBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public PilhaBuilder ComArmazemId(Guid armazemId)
        {
            this._armazemId = armazemId;
            return this;
        }

        public static implicit operator Pilha(PilhaBuilder instance)
        {
            return instance.Build();
        }

    }
}