using s4t.Erp.Core.Domain.Enums;
using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects;
using System;

namespace s4t.Erp.Graos.Domain.Nucleo.Entities
{
    public class FornecedorGraos : Entity
    {
        //Compartilhado com Cadastro
        public FornecedorGraos(Guid id, int codigo, string nome, TipoPFPJ tipoPfpj, CNPJ numeroCnpj, CPF numeroCpf, string numeroRg)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            TipoPfpj = tipoPfpj;
            NumeroCnpj = numeroCnpj;
            NumeroCpf = numeroCpf;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public TipoPFPJ TipoPfpj { get; private set; }
        public CNPJ NumeroCnpj { get; private set; }
        public CPF NumeroCpf { get; private set; }
    }
}
