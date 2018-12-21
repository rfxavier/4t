using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Enums;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Interfaces;
using Xunit;

namespace s4t.Erp.Cadastros.Tests.Unit.Fazendas.Repositories
{
    public class FazendaCertificacaoMovimentacaoRepositoryTests
    {
        [Theory(DisplayName = "GetCertificacoesEmVigencia")]
        [ClassData(typeof(FazendaCertificacaoMovimentacaoRepositoryDadosTeste))]
        [Trait("Category", "FazendaCertificacaoMovimentacaoRepository")]
        public void
            DadaUmaListaDeFazendaCertificacaoMovimentacoes_ComCertosFazendaTipoCulturaDataReferencia_DeveRetornarNumeroEsperadoItensCorreto(
                List<FazendaCertificacao> fazendaCertificacaoMovimentacoesSemFiltro,
                Guid fazendaId, FazendaCertificacaoTipoCultura fazendaCertificacaoTipoCultura, DateTime dataReferencia,
                int numeroItensEsperados)
        {
            //Arrange
            List<FazendaCertificacao> fazendaCertificacaoMovimentacoes =
                new List<FazendaCertificacao>();

            var repo = new Mock<IFazendaCertificacaoMovimentacaoRepository>();
            repo.Setup(x =>
                    x.GetCertificacoesEmVigencia(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Callback(
                    (Guid fazendaIdParam, int fazendaCertificacaoTipoCulturaParam, DateTime dataReferenciaParam) =>
                    {
                        fazendaCertificacaoMovimentacoes = fazendaCertificacaoMovimentacoesSemFiltro.Where(x =>
                            x.Fazenda.Id == fazendaIdParam &&
                            x.FazendaCertificacaoTipoCultura.Value == fazendaCertificacaoTipoCulturaParam &&
                            x.PeriodoVigencia.IsInRange(dataReferenciaParam)).ToList();
                    })
                .Returns(() => fazendaCertificacaoMovimentacoes);


            //Act
            var certificacoesEmVigencia =
                repo.Object.GetCertificacoesEmVigencia(fazendaId, fazendaCertificacaoTipoCultura.Value, dataReferencia);


            //Assert
            Assert.True(certificacoesEmVigencia.Count() == numeroItensEsperados);
        }
    }
}