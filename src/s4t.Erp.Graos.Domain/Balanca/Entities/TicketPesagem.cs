using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Balanca.Entities
{
    public class TicketPesagem : Entity
    {
        private readonly IList<TicketPesagemMovimentacao> _ticketPesagemMovimentacoes;

        public TicketPesagem(Guid id, Guid filialId, string numero, DocumentoEntrada documentoEntrada)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            DocumentoEntrada = documentoEntrada;

            _ticketPesagemMovimentacoes = new List<TicketPesagemMovimentacao>();
        }

        public TicketPesagem(Guid id, Guid filialId, string numero, TicketPortaria ticketPortaria)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            TicketPortaria = ticketPortaria;

            _ticketPesagemMovimentacoes = new List<TicketPesagemMovimentacao>();
        }

        //EF Constructor
        protected TicketPesagem() { }

        public Guid FilialId { get; private set; }
        public string Numero { get; private set; }
        public TicketPortaria TicketPortaria { get; private set; }
        public DocumentoEntrada DocumentoEntrada { get; private set; }

        public ICollection<TicketPesagemMovimentacao> TicketPesagemMovimentacoes
        {
            get { return _ticketPesagemMovimentacoes; }
        }

        public TicketPesagemMovimentacao AdicionaPesagemMovimentacao(TicketPesagemMovimentacao ticketPesagemMovimentacao)
        {
            ticketPesagemMovimentacao.MarcaPesoComUmaCasaDecimal(ticketPesagemMovimentacao.Peso);

            ticketPesagemMovimentacao.MarcaOrdem(_ticketPesagemMovimentacoes.Count + 1);

            var ultimaPesagem = _ticketPesagemMovimentacoes.OrderByDescending(m => m.Ordem).FirstOrDefault();

            // ReSharper disable once NotAccessedVariable
            double pesoUltimaPesagem = 0;

            // ReSharper disable once RedundantAssignment
            if (ultimaPesagem != null) pesoUltimaPesagem = ultimaPesagem.Peso;

            ticketPesagemMovimentacao.MarcaPesoDiferencialAnteriorComValorAbsolutoComUmaCasaDecimal(ticketPesagemMovimentacao.Peso - pesoUltimaPesagem);

            _ticketPesagemMovimentacoes.Add(ticketPesagemMovimentacao);

            return ticketPesagemMovimentacao;
        }
    }
}