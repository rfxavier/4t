using System;
using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Fiscal.Domain.Nucleo.Entities
{
    public class CFOP : Entity
    {
        public CFOP(Guid id, string codigo, string descricao)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
        }

        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
    }
}