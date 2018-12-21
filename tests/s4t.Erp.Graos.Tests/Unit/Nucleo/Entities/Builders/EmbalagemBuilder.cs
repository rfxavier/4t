using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders
{
    public class EmbalagemBuilder
    {
        private Guid _embalagemId = Guid.Empty;
        private int _codigo;
        private string _descricao = string.Empty;
        private double _capacidade = 0;
        private double _peso = 0;

        public Embalagem Build()
        {
            return new Embalagem(_embalagemId, _codigo, _descricao, _capacidade, _peso);
        }

        public EmbalagemBuilder ComEmbalagemId(Guid embalagemId)
        {
            this._embalagemId = embalagemId;
            return this;
        }

        public EmbalagemBuilder ComCodigo(int codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public EmbalagemBuilder ComDescricao(string descricao)
        {
            this._descricao = descricao;
            return this;
        }

        public EmbalagemBuilder ComCapacidade(double capacidade)
        {
            this._capacidade = capacidade;
            return this;
        }

        public EmbalagemBuilder ComPeso(double peso)
        {
            this._peso = peso;
            return this;
        }

        public static implicit operator Embalagem(EmbalagemBuilder instance)
        {
            return instance.Build();
        }

    }
}