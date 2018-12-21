using System;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;

namespace s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders
{
    public class FazendaCertificacaoEmissorBuilder
    {
        private Guid _fazendaCertificacaoEmissorId = Guid.Empty;
        private int _codigo;
        private string _nome = string.Empty;

        public FazendaCertificacaoEmissor Build()
        {
            return new FazendaCertificacaoEmissor(_fazendaCertificacaoEmissorId, _codigo, _nome);
        }

        public FazendaCertificacaoEmissorBuilder ComFazendaCertificacaoEmissorId(Guid fazendaCertificacaoEmissorId)
        {
            this._fazendaCertificacaoEmissorId = fazendaCertificacaoEmissorId;
            return this;
        }

        public FazendaCertificacaoEmissorBuilder ComCodigo(int codigo)
        {
            this._codigo = codigo;
            return this;
        }

        public FazendaCertificacaoEmissorBuilder ComNome(string nome)
        {
            this._nome = nome;
            return this;
        }

        public static implicit operator FazendaCertificacaoEmissor(FazendaCertificacaoEmissorBuilder instance)
        {
            return instance.Build();
        }
    }
}
