using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Entities
{
    public class Armazem : Entity
    {
        public Armazem(Guid id, Guid filialId, string codigo, string enderecoLogradouro, string enderecoNumero,
            string enderecoComplemento, string enderecoBairro, string endereco, CEP cep, Guid cidadeId, int altura,
            int capacidade, string observacao)
        {
            Id = id;
            FilialId = filialId;
            Codigo = codigo;
            EnderecoLogradouro = enderecoLogradouro;
            EnderecoNumero = enderecoNumero;
            EnderecoComplemento = enderecoComplemento;
            EnderecoBairro = enderecoBairro;
            Endereco = endereco;
            Cep = cep;
            CidadeId = cidadeId;
            Altura = altura;
            Capacidade = capacidade;
            Observacao = observacao;
        }

        public Guid FilialId { get; private set; }
        public string Codigo { get; private set; }
        public string EnderecoLogradouro { get; private set; }
        public string EnderecoNumero { get; private set; }
        public string EnderecoComplemento { get; private set; }
        public string EnderecoBairro { get; private set; }
        public string Endereco { get; private set; }
        public CEP Cep { get; private set; }
        public Guid CidadeId { get; private set; }
        public int Altura { get; private set; }
        public int Capacidade { get; private set; }
        public string Observacao { get; private set; }
    }
}