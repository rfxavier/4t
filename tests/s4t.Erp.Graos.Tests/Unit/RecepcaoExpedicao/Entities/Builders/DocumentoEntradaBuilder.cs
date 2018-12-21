using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders
{
    public class DocumentoEntradaBuilder
    {
        private Guid _documentoEntradaId = Guid.Empty;
        private Guid _filialId;
        private int _numero;
        private DateTime _data;
        private TipoOperacao _tipoOperacao;
        private NotaFiscalGraos _notaFiscalGraos;

        private List<DocumentoEntradaEmbalagem> _embalagens = new List<DocumentoEntradaEmbalagem>();
        private List<Lote> _lotes = new List<Lote>();

        public DocumentoEntrada Build()
        {
            var documentoEntrada = new DocumentoEntrada(_documentoEntradaId, _filialId, _numero, _data, _tipoOperacao, _notaFiscalGraos);

            foreach (var documentoEntradaEmbalagem in _embalagens)
            {
                documentoEntrada.AdicionaEmbalagem(documentoEntradaEmbalagem);
            }

            foreach (var lote in _lotes)
            {
                documentoEntrada.AdicionaLote(lote);
            }

            return documentoEntrada;
        }

        public DocumentoEntradaBuilder ComDocumentoEntradaId(Guid documentoEntradaId)
        {
            this._documentoEntradaId = documentoEntradaId;
            return this;
        }

        public DocumentoEntradaBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public DocumentoEntradaBuilder ComNumero(int numero)
        {
            this._numero = numero;
            return this;
        }

        public DocumentoEntradaBuilder ComData(DateTime data)
        {
            this._data = data;
            return this;
        }

        public DocumentoEntradaBuilder ComTipoOperacao(TipoOperacao tipoOperacao)
        {
            this._tipoOperacao = tipoOperacao;
            return this;
        }

        public DocumentoEntradaBuilder ComNotaFiscalGraos(NotaFiscalGraos notaFiscalGraos)
        {
            this._notaFiscalGraos = notaFiscalGraos;
            return this;
        }

        public DocumentoEntradaBuilder ComEmbalagens(List<DocumentoEntradaEmbalagem> embalagens)
        {
            this._embalagens = embalagens;
            return this;
        }

        public DocumentoEntradaBuilder ComLotes(List<Lote> lotes)
        {
            this._lotes = lotes;
            return this;
        }

        public static implicit operator DocumentoEntrada(DocumentoEntradaBuilder instance)
        {
            return instance.Build();
        }
    }
}