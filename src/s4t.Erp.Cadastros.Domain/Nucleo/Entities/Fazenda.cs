using s4t.Erp.Core.Domain.Enums;
using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class Fazenda : Entity
    {
        private readonly IList<FazendaCertificacao> _certificacoes;

        public Fazenda(Guid id, int codigo, string nome, CNPJ cnpj, DateTime dataCadastro, DateTime dataBaixa,
            string observacao, string enderecoLogradouro, string enderecoNumero, string enderecoComplemento,
            string enderecoBairro, string endereco, CEP cep, Cidade cidade, Telefone telefone, Telefone telefoneAux,
            string codigoInscricaoEstadualProdutorRural, DateTime dataValidadeCartaoProdutorRural,
            FazendaStatus fazendaStatus, Cadastro cadastro)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            Cnpj = cnpj;
            DataCadastro = dataCadastro;
            DataBaixa = dataBaixa;
            Observacao = observacao;
            EnderecoLogradouro = enderecoLogradouro;
            EnderecoNumero = enderecoNumero;
            EnderecoComplemento = enderecoComplemento;
            EnderecoBairro = enderecoBairro;
            Endereco = endereco;
            Cep = cep;
            Cidade = cidade;
            Telefone = telefone;
            TelefoneAux = telefoneAux;
            CodigoInscricaoEstadualProdutorRural = codigoInscricaoEstadualProdutorRural;
            DataValidadeCartaoProdutorRural = dataValidadeCartaoProdutorRural;
            FazendaStatus = fazendaStatus;
            Cadastro = cadastro;

            _certificacoes = new List<FazendaCertificacao>();
        }

        // Empty constructor for EF
        protected Fazenda()
        {
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public CNPJ Cnpj { get; private set; }
        public string EnderecoLogradouro { get; private set; }
        public string EnderecoNumero { get; private set; }
        public string EnderecoComplemento { get; private set; }
        public string EnderecoBairro { get; private set; }
        public string Endereco { get; private set; }
        public CEP Cep { get; private set; }
        public Cidade Cidade { get; private set; }
        public Telefone Telefone { get; private set; }
        public Telefone TelefoneAux { get; private set; }
        public string CodigoInscricaoEstadualProdutorRural { get; private set; }
        public DateTime DataValidadeCartaoProdutorRural { get; private set; }
        public FazendaStatus FazendaStatus { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataBaixa { get; private set; }
        public string Observacao { get; private set; }
        public Cadastro Cadastro { get; private set; }

        public ICollection<FazendaCertificacao> Certificacoes
        {
            get { return _certificacoes; }
        }

        public void AdicionaCertificacao(FazendaCertificacao certificacao)
        {
            _certificacoes.Add(certificacao);
        }
    }
}