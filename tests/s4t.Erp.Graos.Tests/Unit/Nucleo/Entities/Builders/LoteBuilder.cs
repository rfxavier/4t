using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders
{
    public class LoteBuilder
    {
        private Guid _loteId = Guid.Empty;
        private Guid _filialId = Guid.Empty;
        private string _numero = string.Empty;
        private int _sacas = 0;
        private double _peso = 0;
        private TicketPesagemMovimentacao _ticketPesagemMovimentacao = null;
        private TipoGrao _tipoGrao = null;
        private Embalagem _embalagem = null;

        private Localizacao _localizacao = new Localizacao(Guid.Empty, null, string.Empty, string.Empty, null, null);

        private Guid _cadastroTitularId = Guid.Empty;
        private Guid _fazendaTitularId = Guid.Empty;
        private DocumentoEntrada _documentoEntrada = null;

        public Lote Build()
        {
            return new Lote(_loteId, _filialId, _numero, _sacas, _peso, _tipoGrao, _embalagem,
                _ticketPesagemMovimentacao, _cadastroTitularId, _fazendaTitularId, _documentoEntrada);
        }

        public LoteBuilder ComLoteId(Guid loteId)
        {
            this._loteId = loteId;
            return this;
        }

        public LoteBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public LoteBuilder ComNumero(string numero)
        {
            this._numero = numero;
            return this;
        }

        public LoteBuilder ComSacas(int sacas)
        {
            this._sacas = sacas;
            return this;
        }

        public LoteBuilder ComPeso(double peso)
        {
            this._peso = peso;
            return this;
        }

        public LoteBuilder ComTicketPesagemMovimentacao(TicketPesagemMovimentacao ticketPesagemMovimentacao)
        {
            this._ticketPesagemMovimentacao = ticketPesagemMovimentacao;
            return this;
        }

        public LoteBuilder ComTipoGrao(TipoGrao tipoGrao)
        {
            this._tipoGrao = tipoGrao;
            return this;
        }

        public LoteBuilder ComEmbalagem(Embalagem embalagem)
        {
            this._embalagem = embalagem;
            return this;
        }

        public LoteBuilder ComLocalizacao(Localizacao localizacao)
        {
            this._localizacao = localizacao;
            return this;
        }

        public LoteBuilder ComCadastroTitularId(Guid cadastroTitularId)
        {
            this._cadastroTitularId = cadastroTitularId;
            return this;
        }

        public LoteBuilder ComFazendaTitularId(Guid fazendaTitularId)
        {
            this._fazendaTitularId = fazendaTitularId;
            return this;
        }

        public LoteBuilder ComDocumentoEntrada(DocumentoEntrada documentoEntrada)
        {
            this._documentoEntrada = documentoEntrada;
            return this;
        }

        public static implicit operator Lote(LoteBuilder instance)
        {
            return instance.Build();
        }
    }
}