using s4t.Erp.Core.Domain.ValueObjects;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders
{
    public class MotoristaBuilder
    {
        private Guid _motoristaId = Guid.Empty;
        private int _codigo;
        private string _nome = string.Empty;
        private CPF _cpf = new CPF(string.Empty);

        public Motorista Build()
        {
            return new Motorista(_motoristaId, _codigo, _nome, _cpf);
        }

        public MotoristaBuilder ComMotoristaId(Guid motoristaId)
        {
            this._motoristaId = motoristaId;
            return this;
        }

        public MotoristaBuilder ComCodigo(int codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public MotoristaBuilder ComNome(string nome)
        {
            this._nome = nome;
            return this;
        }

        public MotoristaBuilder ComCPF(CPF cpf)
        {
            this._cpf = cpf;
            return this;
        }

        public static implicit operator Motorista(MotoristaBuilder instance)
        {
            return instance.Build();
        }
    }
}