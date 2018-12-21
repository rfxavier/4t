using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders
{
    public class EmpresaBuilder
    {
        private Guid _empresaId = Guid.Empty;
        private int _codigo;
        private string _nome = string.Empty;

        public Empresa Build()
        {
            return new Empresa(_empresaId, _codigo, _nome);
        }

        public EmpresaBuilder ComEmpresaId(Guid empresaId)
        {
            this._empresaId = empresaId;
            return this;
        }

        public EmpresaBuilder ComCodigo(int codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public EmpresaBuilder ComNome(string nome)
        {
            this._nome = nome;
            return this;
        }

        public static implicit operator Empresa(EmpresaBuilder instance)
        {
            return instance.Build();
        }
    }
}