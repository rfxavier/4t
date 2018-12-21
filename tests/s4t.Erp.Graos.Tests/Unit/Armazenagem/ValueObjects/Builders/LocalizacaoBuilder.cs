using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects.Builders
{
    public class LocalizacaoBuilder
    {
        private Guid _filialId = Guid.Empty;
        private Armazem _armazem = null;
        private string _quadra;
        private string _bloco;
        private Local _local = null;
        private Pilha _pilha = null;

        public Localizacao Build()
        {
            return new Localizacao(_filialId, _armazem, _quadra, _bloco, _local, _pilha);
        }

        public LocalizacaoBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public LocalizacaoBuilder ComArmazem(Armazem armazem)
        {
            this._armazem = armazem;
            return this;
        }

        public LocalizacaoBuilder ComLocal(Local local)
        {
            this._local = local;
            return this;
        }

        public LocalizacaoBuilder ComPilha(Pilha pilha)
        {
            this._pilha = pilha;
            return this;
        }

        public LocalizacaoBuilder ComQuadra(string quadra)
        {
            this._quadra = quadra;
            return this;
        }
        public LocalizacaoBuilder ComBloco(string bloco)
        {
            this._bloco = bloco;
            return this;
        }

        public static implicit operator Localizacao(LocalizacaoBuilder instance)
        {
            return instance.Build();
        }
    }
}