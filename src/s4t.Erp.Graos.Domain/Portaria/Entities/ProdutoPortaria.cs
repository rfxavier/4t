using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Entities
{
    public class ProdutoPortaria : Entity
    {
        public ProdutoPortaria(Guid id, int codigo, string descricao)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
    }
}