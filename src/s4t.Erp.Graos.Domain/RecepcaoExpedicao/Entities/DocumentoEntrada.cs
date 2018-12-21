using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Enums;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities
{
    public class DocumentoEntrada : Entity
    {
        private readonly IList<Lote> _lotes;
        private readonly IList<DocumentoEntradaEmbalagem> _embalagens;

        public DocumentoEntrada(Guid id, Guid filialId, int numero, DateTime data, TipoOperacao tipoOperacao,
            NotaFiscalGraos notaFiscalGraos)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            Data = data;
            TipoOperacao = tipoOperacao;
            NotaFiscalGraos = notaFiscalGraos;

            DocumentoEntradaStatus = DocumentoEntradaStatus.Aberto;

            _lotes = new List<Lote>();
            _embalagens = new List<DocumentoEntradaEmbalagem>();

            TicketPesagem = null;
        }

        //Parameterless constructor para EF
        protected DocumentoEntrada() { }

        public Guid FilialId { get; private set; }
        public int Numero { get; private set; }
        public DateTime Data { get; private set; }
        public TipoOperacao TipoOperacao { get; private set; }
        public NotaFiscalGraos NotaFiscalGraos { get; private set; }
        public DocumentoEntradaStatus DocumentoEntradaStatus { get; private set; }
        public TicketPesagem TicketPesagem { get; private set; }

        public void TrocaStatusParaComplementadoComLotesEPesos()
        {
            DocumentoEntradaStatus = DocumentoEntradaStatus.ComplementadoComLotesEPesos;
        }

        public ICollection<Lote> Lotes
        {
            get { return _lotes; }
        }

        public ICollection<DocumentoEntradaEmbalagem> Embalagens
        {
            get { return _embalagens; }
        }

        public void AdicionaLote(Lote lote)
        {
            _lotes.Add(lote);
        }

        public void AdicionaEmbalagem(DocumentoEntradaEmbalagem embalagens)
        {
            _embalagens.Add(embalagens);
        }
    }
}