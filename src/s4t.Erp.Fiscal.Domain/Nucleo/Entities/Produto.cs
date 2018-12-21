using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Fiscal.Domain.Nucleo.Entities
{
    public class Produto : Entity
    {
        protected Produto() { }

        public Produto(Guid id, int codigo, string descricao, ProdutoUnidade produtoUnidade)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            ProdutoUnidade = produtoUnidade;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public ProdutoUnidade ProdutoUnidade { get; private set; }
    }
}