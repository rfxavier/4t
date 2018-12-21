using s4t.Erp.Graos.Domain.Portaria.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Entities.Builders
{
    public class ProdutoPortariaBuilder
    {
        private Guid _produtoPortariaId = Guid.Empty;
        private int _codigo;
        private string _descricao = string.Empty;

        public ProdutoPortaria Build()
        {
            return new ProdutoPortaria(_produtoPortariaId, _codigo, _descricao);
        }

        public ProdutoPortariaBuilder ComProdutoPortariaId(Guid produtoPortariaId)
        {
            this._produtoPortariaId = produtoPortariaId;
            return this;
        }

        public ProdutoPortariaBuilder ComCodigo(int codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public ProdutoPortariaBuilder ComDescricao(string descricao)
        {
            this._descricao = descricao;
            return this;
        }

        public static implicit operator ProdutoPortaria(ProdutoPortariaBuilder instance)
        {
            return instance.Build();
        }
    }
}