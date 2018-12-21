using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects;
using System;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class Filial: Entity
    {
        public Filial(Guid id, int codigo, string nome, CNPJ cnpj, Guid empresaId, Empresa empresa)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            Cnpj = cnpj;
            EmpresaId = empresaId;
            Empresa = empresa;
        }

        // Empty constructor for EF
        protected Filial(Empresa empresa)
        {
            Empresa = empresa;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public CNPJ Cnpj { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
    }
}
