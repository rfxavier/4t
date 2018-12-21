using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.ValueObjects.DateTimeRange;
using System;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Enums;

namespace s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders
{
    public class FazendaCertificacaoMovimentacaoBuilder
    {
        private Guid _fazendaCertificacaoMovimentacaoId = Guid.Empty;
        private Guid _fazendaId;
        private Fazenda _fazenda;
        private FazendaCertificacaoEmissor _fazendaCertificacaoEmissor;
        private string _certificacaoNumero = string.Empty;
        private FazendaCertificacaoTipoCultura _fazendaCertificacaoTipoCultura;
        private DateTimeRange _periodoVigencia;

        public FazendaCertificacao Build()
        {
            return new FazendaCertificacao(_fazendaCertificacaoMovimentacaoId, _fazendaId, _fazenda,
                _fazendaCertificacaoEmissor, _certificacaoNumero, _fazendaCertificacaoTipoCultura, _periodoVigencia);
        }

        public FazendaCertificacaoMovimentacaoBuilder ComFazendaCertificacaoMovimentacaoId(
            Guid fazendaCertificacaoMovimentacaoId)
        {
            this._fazendaCertificacaoMovimentacaoId = fazendaCertificacaoMovimentacaoId;
            return this;
        }

        public FazendaCertificacaoMovimentacaoBuilder ComFazendaId(Guid fazendaId)
        {
            this._fazendaId = fazendaId;
            return this;
        }

        public FazendaCertificacaoMovimentacaoBuilder ComFazenda(Fazenda fazenda)
        {
            this._fazenda = fazenda;
            return this;
        }

        public FazendaCertificacaoMovimentacaoBuilder ComFazendaCertificacaoEmissor(
            FazendaCertificacaoEmissor fazendaCertificacaoEmissor)
        {
            this._fazendaCertificacaoEmissor = fazendaCertificacaoEmissor;
            return this;
        }

        public FazendaCertificacaoMovimentacaoBuilder ComCertificacaoNumero(string certificacaoNumero)
        {
            this._certificacaoNumero = certificacaoNumero;
            return this;
        }

        public FazendaCertificacaoMovimentacaoBuilder ComFazendaCertificacaoTipoCultura(
            FazendaCertificacaoTipoCultura fazendaCertificacaoTipoCultura)
        {
            this._fazendaCertificacaoTipoCultura = fazendaCertificacaoTipoCultura;
            return this;
        }

        public FazendaCertificacaoMovimentacaoBuilder ComPeriodoVigencia(DateTimeRange periodoVigencia)
        {
            this._periodoVigencia = periodoVigencia;
            return this;
        }

        public static implicit operator FazendaCertificacao(
            FazendaCertificacaoMovimentacaoBuilder instance)
        {
            return instance.Build();
        }
    }
}