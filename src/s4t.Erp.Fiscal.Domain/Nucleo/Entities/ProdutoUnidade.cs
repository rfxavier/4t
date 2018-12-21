using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Fiscal.Domain.Nucleo.Entities
{
    public class ProdutoUnidade : Entity
    {
        protected ProdutoUnidade() { }

        public ProdutoUnidade(Guid id, string codigo, string descricao)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
        }

        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
    }
}