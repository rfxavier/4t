using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Entities
{
    public class Local : Entity
    {
        public Local(Guid id, Guid empresaId, string codigo, string descricao)
        {
            Id = id;
            EmpresaId = empresaId;
            Codigo = codigo;
            Descricao = descricao;
        }

        public Guid EmpresaId { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
    }
}