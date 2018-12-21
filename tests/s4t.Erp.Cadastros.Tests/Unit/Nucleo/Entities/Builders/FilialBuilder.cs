using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.ValueObjects;
using System;

namespace s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders
{
    public class FilialBuilder
    {
        private Guid _filialId = Guid.Empty;
        private int _codigo;
        private string _nome = string.Empty;
        private CNPJ _cnpj = new CNPJ(string.Empty);
        private Guid _empresaId = Guid.Empty;
        private Empresa _empresa = new EmpresaBuilder();

        public Filial Build()
        {
            return new Filial(_filialId, _codigo, _nome, _cnpj, _empresaId, _empresa);
        }

        public FilialBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public FilialBuilder ComCodigo(int codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public FilialBuilder ComNome(string nome)
        {
            this._nome = nome;
            return this;
        }

        public FilialBuilder ComCNPJ(CNPJ cnpj)
        {
            this._cnpj = cnpj;
            return this;
        }

        public FilialBuilder ComEmpresaId(Guid empresaId)
        {
            this._empresaId = empresaId;
            return this;
        }

        public FilialBuilder ComEmpresa(Empresa empresa)
        {
            this._empresa = empresa;
            return this;
        }

        public static implicit operator Filial(FilialBuilder instance)
        {
            return instance.Build();
        }
    }
}