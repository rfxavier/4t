using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders
{
    public class FazendaBuilder
    {
        private Guid _fazendaId = Guid.Empty;
        private int _codigo;
        private string _nome = string.Empty;

        public Fazenda Build()
        {
            return new Fazenda(_fazendaId, _codigo, _nome, null, DateTime.MinValue, DateTime.MinValue, null, null, null,
                null, null, null, null, null, null, null, null, DateTime.MinValue, null, null);
        }

        public FazendaBuilder ComFazendaId(Guid fazendaId)
        {
            this._fazendaId = fazendaId;
            return this;
        }

        public FazendaBuilder ComCodigo(int codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public FazendaBuilder ComNome(string nome)
        {
            this._nome = nome;
            return this;
        }

        public static implicit operator Fazenda(FazendaBuilder instance)
        {
            return instance.Build();
        }
    }
}