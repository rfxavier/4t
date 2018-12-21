using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Fiscal.Domain.Nucleo.Enums;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Fiscal.Domain.Nucleo.Entities
{
    public class NotaFiscal : Entity
    {
        private readonly IList<Produto> _produtos;

        public NotaFiscal(Guid id, Guid filialId, string numero, string serie, DateTime dataEmissao,
            DateTime dataEntrada, DateTime dataSaida, NotaFiscalTipo notaFiscalTipo,
            NotaFiscalModoInclusao notaFiscalModoInclusao, Guid emitenteId, Guid destinatarioId,
            Guid destinatarioFazendaId, CFOP cfop)
        {
            Id = id;
            FilialId = filialId;
            Numero = numero;
            Serie = serie;
            DataEmissao = dataEmissao;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            NotaFiscalTipo = notaFiscalTipo;
            NotaFiscalModoInclusao = notaFiscalModoInclusao;
            EmitenteId = emitenteId;
            DestinatarioId = destinatarioId;
            DestinatarioFazendaId = destinatarioFazendaId;
            Cfop = cfop;

            _produtos = new List<Produto>();
        }

        protected NotaFiscal()
        {
        }

        public Guid FilialId { get; private set; }
        public string Numero { get; private set; }
        public string Serie { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime DataSaida { get; private set; }
        public DateTime DataES { get; private set; }
        public NotaFiscalTipo NotaFiscalTipo { get; private set; }
        public NotaFiscalModoInclusao NotaFiscalModoInclusao { get; private set; }

        public Guid EmitenteId { get; private set; }

        //public Cadastro Emitente { get; private set; }
        public Guid DestinatarioId { get; private set; }

        //public Cadastro Destinatario { get; private set; }
        public Guid DestinatarioFazendaId { get; private set; }

        //public Fazenda DestinatarioFazenda { get; private set; }
        public CFOP Cfop { get; private set; }

        public ICollection<Produto> Produtos
        {
            get { return _produtos; }
        }

        public void AdicionaProduto(Produto produto)
        {
            _produtos.Add(produto);
        }
    }
}