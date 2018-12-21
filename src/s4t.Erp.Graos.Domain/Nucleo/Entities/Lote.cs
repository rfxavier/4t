using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.Nucleo.Entities
{
    public class Lote : Entity
    {
        private readonly IList<LoteEmbalagemNumeracao> _loteEmbalagemNumeracoes;
        private readonly IList<Boletim> _boletins;

        /// <summary>
        /// Para criar novo lote sem localização (Documento Entrada)
        /// </summary>
        public Lote(Guid id, Guid filialId, string numero, int sacas, double peso, TipoGrao tipoGrao,
            Embalagem embalagem,
            TicketPesagemMovimentacao ticketPesagemMovimentacao, Guid cadastroTitularId, Guid fazendaTitularId,
            DocumentoEntrada documentoEntrada)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            Sacas = sacas;
            Peso = peso;
            TipoGrao = tipoGrao;
            Embalagem = embalagem;
            TicketPesagemMovimentacao = ticketPesagemMovimentacao;
            Localizacao = new Localizacao(Guid.Empty, null, string.Empty, string.Empty, null, null);
            CadastroTitularId = cadastroTitularId;
            FazendaTitularId = fazendaTitularId;
            DocumentoEntrada = documentoEntrada;

            _loteEmbalagemNumeracoes = new List<LoteEmbalagemNumeracao>();
            _boletins = new List<Boletim>();
        }

        /// <summary>
        /// Para criar novo lote com localização (Boletim)
        /// </summary>
        public Lote(Guid id, Guid filialId, string numero, int sacas, double peso, TipoGrao tipoGrao,
            Localizacao localizacao, Guid cadastroTitularId, Guid fazendaTitularId,
            DocumentoEntrada documentoEntrada)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            Sacas = sacas;
            Peso = peso;
            TipoGrao = tipoGrao;
            Localizacao = localizacao;
            CadastroTitularId = cadastroTitularId;
            FazendaTitularId = fazendaTitularId;
            DocumentoEntrada = documentoEntrada;

            _loteEmbalagemNumeracoes = new List<LoteEmbalagemNumeracao>();
        }

        public Guid FilialId { get; private set; }
        public string Numero { get; private set; }
        public int Sacas { get; private set; }
        public double Peso { get; private set; }
        public TicketPesagemMovimentacao TicketPesagemMovimentacao { get; private set; }
        public TipoGrao TipoGrao { get; private set; }

        //todo controlar quantidade de embalagem
        public Embalagem Embalagem { get; private set; }

        //todo localização vai ficar na numeração de embalagem do lote, sempre vai haver uma padrão
        public Localizacao Localizacao { get; private set; }

        public Guid CadastroTitularId { get; private set; }

        //public Cadastro CadastroTitular { get; private set; }
        public Guid FazendaTitularId { get; private set; }

        //public Fazenda FazendaTitular { get; private set; }
        public DocumentoEntrada DocumentoEntrada { get; private set; }

        public ICollection<LoteEmbalagemNumeracao> LoteEmbalagemNumeracoes
        {
            get { return _loteEmbalagemNumeracoes; }
        }

        public void AdicionaEmbalagemNumeracao(LoteEmbalagemNumeracao loteEmbalagemNumeracao)
        {
            _loteEmbalagemNumeracoes.Add(loteEmbalagemNumeracao);
        }

        public ICollection<Boletim> Boletins
        {
            get { return _boletins; }
        }

        public void AdicionaBoletim(Boletim boletim)
        {
            _boletins.Add(boletim);
        }

        public void AlteraLocalizacao(Localizacao localizacao)
        {
            Localizacao = localizacao;
        }

    }
}