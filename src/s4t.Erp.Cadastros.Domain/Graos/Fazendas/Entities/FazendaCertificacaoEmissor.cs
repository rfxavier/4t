using System;
using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities
{
    public class FazendaCertificacaoEmissor : Entity
    {
        public FazendaCertificacaoEmissor(Guid id, int codigo, string nome)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
    }
}
