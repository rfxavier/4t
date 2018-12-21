using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.Nucleo.Entities
{
    public class NotaFiscalGraos : Entity
    {
        private readonly IList<NotaFiscalGraosCertificado> _notaFiscalGraosCertificados;

        public NotaFiscalGraos()
        {
            Id = Guid.NewGuid();

            _notaFiscalGraosCertificados = new List<NotaFiscalGraosCertificado>();
        }

        public DocumentoEntrada DocumentoEntrada { get; set; }
        public Guid? RegistroDePortariaId { get; set; }
        public RegistroDePortaria RegistroDePortaria { get; set; }
        public Guid NotaFiscalId { get; set; }
        public string NotaFiscalNumero { get; set; }
        public string NotaFiscalSerie { get; set; }
        public DateTime NotaFiscalDataEmissao { get; set; }
        public DateTime NotaFiscalDataSaida { get; set; }
        public string NotaFiscalOperacaoFiscalCodigo { get; set; }
        public string NotaFiscalOperacaoFiscalDescricao { get; set; }
        public string NotaFiscalObservacoes { get; set; }
        public int NotaFiscalGraosTipo { get; set; }
        public string NotaFiscalGraosTipoDescricao { get; set; }
        public int TipoGrao { get; set; }
        public int EmpresaCodigo { get; set; }
        public string EmpresaNome { get; set; }
        public Guid FilialId { get; set; }
        public int FilialCodigo { get; set; }
        public string FilialDescricao { get; set; }
        public string FilialEndereco { get; set; }
        public string FilialCidade { get; set; }
        public string FilialUf { get; set; }
        public string FilialCnpj { get; set; }
        public string FilialInscricaoEstadual { get; set; }
        public Guid DestinatarioCadastroId { get; set; }
        public int DestinatarioCodigo { get; set; }
        public string DestinatarioNome { get; set; }
        public string DestinatarioCpf { get; set; }
        public Guid DestinatarioFazendaId { get; set; }
        public int DestinatarioFazendaCodigo { get; set; }
        public string DestinatarioFazendaNome { get; set; }
        public string DestinatarioFazendaInscricaoEstadual { get; set; }
        public string DestinatarioFazendaCnpj { get; set; }
        public string DestinatarioFazendaMunicipio { get; set; }
        public string DestinatarioFazendaUf { get; set; }
        public double PesoLiquido { get; set; }
        public Guid EmbalagemId { get; set; }
        public int EmbalagemCodigo { get; set; }
        public string EmbalagemDescricao { get; set; }
        public int EmbalagemQuantidade { get; set; }

        public ICollection<NotaFiscalGraosCertificado> NotaFiscalGraosCertificados
        {
            get { return _notaFiscalGraosCertificados; }
        }

        public void AdicionaCertificado(NotaFiscalGraosCertificado notaFiscalGraosCertificado)
        {
            _notaFiscalGraosCertificados.Add(notaFiscalGraosCertificado);
        }

        public bool PossuiCertificados()
        {
            return _notaFiscalGraosCertificados.Count > 0;
        }

    }
}