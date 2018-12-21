using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Tests.Unit.Balanca.Entities.Builders
{
    public class TicketPesagemBuilder
    {
        private Guid _ticketPesagemId = Guid.Empty;
        private Guid _filialId;
        private string _numero;
        private TicketPortaria _ticketPortaria;
        private DocumentoEntrada _documentoEntrada;

        private ICollection<TicketPesagemMovimentacao> _movimentacoes = new List<TicketPesagemMovimentacao>();

        public TicketPesagem Build()
        {
            TicketPesagem ticketPesagem = null;

            if (_documentoEntrada != null)
            {
                ticketPesagem = new TicketPesagem(_ticketPesagemId, _filialId, _numero, _documentoEntrada);

                foreach (var ticketPesagemMovimentacao in _movimentacoes)
                {
                    ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);
                }

                return ticketPesagem;
            }

            if (_ticketPortaria != null)
            {
                ticketPesagem = new TicketPesagem(_ticketPesagemId, _filialId, _numero, _ticketPortaria);

                foreach (var ticketPesagemMovimentacao in _movimentacoes)
                {
                    ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);
                }

                return ticketPesagem;

            }

           //Construtor e retorno default
            ticketPesagem = new TicketPesagem(_ticketPesagemId, _filialId, _numero, _documentoEntrada);

            foreach (var ticketPesagemMovimentacao in _movimentacoes)
            {
                ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);
            }

            return ticketPesagem;
        }

        public TicketPesagemBuilder ComTicketPesagemId(Guid ticketPesagemId)
        {
            this._ticketPesagemId = ticketPesagemId;
            return this;
        }

        public TicketPesagemBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public TicketPesagemBuilder ComNumero(string numero)
        {
            this._numero = numero;
            return this;
        }

        public TicketPesagemBuilder ComTicketPortaria(TicketPortaria ticketPortaria)
        {
            this._ticketPortaria = ticketPortaria;
            return this;
        }

        public TicketPesagemBuilder ComDocumentoEntrada(DocumentoEntrada documentoEntrada)
        {
            this._documentoEntrada = documentoEntrada;
            return this;
        }

        public TicketPesagemBuilder ComMovimentacoes(ICollection<TicketPesagemMovimentacao> movimentacoes)
        {
            this._movimentacoes = movimentacoes;
            return this;
        }

        public static implicit operator TicketPesagem(TicketPesagemBuilder instance)
        {
            return instance.Build();
        }
    }
}