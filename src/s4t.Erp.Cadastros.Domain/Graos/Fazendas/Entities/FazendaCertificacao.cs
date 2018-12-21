using System;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Enums;
using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects.DateTimeRange;

namespace s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities
{
    public class FazendaCertificacao : Entity
    {
        public FazendaCertificacao(Guid id, Guid fazendaId, Fazenda fazenda, FazendaCertificacaoEmissor certificacaoEmissor,
            string certificacaoNumero, FazendaCertificacaoTipoCultura fazendaCertificacaoTipoCultura, DateTimeRange periodoVigencia)
        {
            Id = id;
            FazendaId = fazendaId;
            Fazenda = fazenda;
            CertificacaoEmissor = certificacaoEmissor;
            CertificacaoNumero = certificacaoNumero;
            FazendaCertificacaoTipoCultura = fazendaCertificacaoTipoCultura;
            PeriodoVigencia = periodoVigencia;
        }

        public Guid FazendaId { get; private set; }
        public Fazenda Fazenda { get; private set; }
        public FazendaCertificacaoEmissor CertificacaoEmissor { get; private set; }
        public string CertificacaoNumero { get; private set; }
        public FazendaCertificacaoTipoCultura FazendaCertificacaoTipoCultura { get; private set; }
        public DateTimeRange PeriodoVigencia { get; private set; }
    }
}