using s4t.Erp.Core.Domain.Enums;
using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class Cadastro : Entity
    {
        private readonly IList<Fazenda> _fazendas;

        public Cadastro(Guid id, int codigo, string nome, TipoPFPJ tipoPfpj, CNPJ numeroCnpj, CPF numeroCpf,
            string numeroRg, string numeroInscricaoEstadual, string enderecoLogradouro, string enderecoNumero,
            string enderecoComplemento, string enderecoBairro, string endereco, CEP cep, Cidade cidade, Email email,
            DateTime dataNascimento, string nomeFantasia)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            TipoPfpj = tipoPfpj;
            NumeroCnpj = numeroCnpj;
            NumeroCpf = numeroCpf;
            NumeroRg = numeroRg;
            NumeroInscricaoEstadual = numeroInscricaoEstadual;
            EnderecoLogradouro = enderecoLogradouro;
            EnderecoNumero = enderecoNumero;
            EnderecoComplemento = enderecoComplemento;
            EnderecoBairro = enderecoBairro;
            Endereco = endereco;
            Cep = cep;
            Cidade = cidade;
            Email = email;
            DataNascimento = dataNascimento;
            NomeFantasia = nomeFantasia;

            _fazendas = new List<Fazenda>();
        }

        // Empty constructor for EF
        protected Cadastro() { }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public TipoPFPJ TipoPfpj { get; private set; }
        public CNPJ NumeroCnpj { get; private set; }
        public CPF NumeroCpf { get; private set; }
        public string NumeroRg { get; private set; }
        public string NumeroInscricaoEstadual { get; private set; }
        public string EnderecoLogradouro { get; private set; }
        public string EnderecoNumero { get; private set; }
        public string EnderecoComplemento { get; private set; }
        public string EnderecoBairro { get; private set; }
        public string Endereco { get; private set; }
        public CEP Cep { get; private set; }
        public Cidade Cidade { get; private set; }
        public Email Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string NomeFantasia { get; private set; }

        public Usuario Usuario { get; private set; }
        public ICollection<Fazenda> Fazendas
        {
            get { return _fazendas; }
        }

        public void AdicionaFazenda(Fazenda fazenda)
        {
            _fazendas.Add(fazenda);
        }
    }
}