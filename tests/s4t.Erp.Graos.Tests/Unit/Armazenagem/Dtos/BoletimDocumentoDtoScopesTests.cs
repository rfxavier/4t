using Moq;
using s4t.Erp.Graos.Domain.Armazenagem.Dtos;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Dtos
{
    public class BoletimDocumentoDtoScopesTests
    {
        private class BoletimDocumentoSerieGerador : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    "NDE", BoletimSerie.Nde
                },
                new object[]
                {
                    "nde", BoletimSerie.Nde
                },
                new object[]
                {
                    "is", BoletimSerie.Is
                },
                new object[]
                {
                    "IS", BoletimSerie.Is
                },
                new object[]
                {
                    "TIS", BoletimSerie.Tis
                },
                new object[]
                {
                    "tis", BoletimSerie.Tis
                },
                new object[]
                {
                    "TR", BoletimSerie.Tr
                },
                new object[]
                {
                    "tr", BoletimSerie.Tr
                },
                new object[]
                {
                    "RE", BoletimSerie.Re
                },
                new object[]
                {
                    "re", BoletimSerie.Re
                },
                new object[]
                {
                    "OC", BoletimSerie.Oc
                },
                new object[]
                {
                    "oc", BoletimSerie.Oc
                }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class BoletimDocumentoGerador : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    "NDE",
                    new BoletimDocumento(BoletimSerie.Nde,
                        new DocumentoEntradaBuilder()
                            .ComDocumentoEntradaId(Guid.NewGuid()), null, null, null, 0)
                },
                new object[]
                {
                    "IS",
                    new BoletimDocumento(BoletimSerie.Is, null,
                        new InstrucaoServicoBuilder().ComInstrucaoServicoId(Guid.NewGuid()), null, null, 0)
                },
                new object[]
                {
                    "TIS",
                    new BoletimDocumento(BoletimSerie.Tis, null, null,
                        new TransferenciaBuilder().ComTransferenciaId(Guid.NewGuid()), null, 0)
                },
                new object[]
                {
                    "TR",
                    new BoletimDocumento(BoletimSerie.Tr, null, null,
                        new TransferenciaBuilder().ComTransferenciaId(Guid.NewGuid()), null, 0)
                },
                new object[]
                {
                    "RE",
                    new BoletimDocumento(BoletimSerie.Re, null, null, null, null, 0)
                },
                new object[]
                {
                    "OC",
                    new BoletimDocumento(BoletimSerie.Oc, null, null, null,
                        new OrdemCarregamentoBuilder().ComOrdemCarregamentoId(Guid.NewGuid()), 0)
                }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        [Theory(DisplayName = "ObterBoletimDocumento")]
        [InlineData("")]
        [InlineData("ZXZ")]
        [Trait("Category", "BoletimDocumentoDto Scopes")]
        void
            BoletimDocumentoDto_ComSerieInvalida_ObterBoletimDocumento_DeveRetornar_BoletimDocumentoValueObjectVazio(
                string serie)
        {
            //Arrange
            var boletimDocumentoDto = new BoletimDocumentoDto()
            {
                Serie = serie
            };

            var documentoEntradaRepositoryMock = new Mock<IDocumentoEntradaRepository>();
            var instrucaoServicoRepositoryMock = new Mock<IInstrucaoServicoRepository>();
            var transferenciaRepositoryMock = new Mock<ITransferenciaRepository>();
            var ordemCarregamentoRepositoryMock = new Mock<IOrdemCarregamentoRepository>();

            var boletimDocumentoVazio = new BoletimDocumento(null, null, null, null, null, 0);

            //Act
            var boletimDocumento = boletimDocumentoDto.ObterBoletimDocumento(documentoEntradaRepositoryMock.Object,
                instrucaoServicoRepositoryMock.Object, transferenciaRepositoryMock.Object,
                ordemCarregamentoRepositoryMock.Object);

            //Assert
            Assert.Equal(boletimDocumentoVazio, boletimDocumento);
        }

        [Theory(DisplayName = "ObterBoletimDocumento")]
        [ClassData(typeof(BoletimDocumentoSerieGerador))]
        [Trait("Category", "BoletimDocumentoDto Scopes")]
        void
            BoletimDocumentoDto_ComSerieValida_QueNaoAcheDocumentos_ObterBoletimDocumento_DeveRetornar_BoletimDocumentoValueObjectVazio(
                string serie, BoletimSerie boletimSerie)
        {
            //Arrange
            var boletimDocumentoDto = new BoletimDocumentoDto()
            {
                Serie = serie
            };

            var documentoEntradaRepositoryMock = new Mock<IDocumentoEntradaRepository>();

            DocumentoEntrada documentoEntrada = null;

            documentoEntradaRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => documentoEntrada);


            var instrucaoServicoRepositoryMock = new Mock<IInstrucaoServicoRepository>();

            InstrucaoServico instrucaoServico = null;

            instrucaoServicoRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => instrucaoServico);


            var transferenciaRepositoryMock = new Mock<ITransferenciaRepository>();

            Transferencia transferencia = null;

            transferenciaRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => transferencia);


            var ordemCarregamentoRepositoryMock = new Mock<IOrdemCarregamentoRepository>();

            OrdemCarregamento ordemCarregamento = null;

            ordemCarregamentoRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => ordemCarregamento);


            var boletimDocumentoEsperado = new BoletimDocumento(boletimSerie, null, null, null, null, 0);

            //Act
            var boletimDocumento = boletimDocumentoDto.ObterBoletimDocumento(documentoEntradaRepositoryMock.Object,
                instrucaoServicoRepositoryMock.Object, transferenciaRepositoryMock.Object,
                ordemCarregamentoRepositoryMock.Object);

            //Assert
            Assert.Equal(boletimDocumentoEsperado, boletimDocumento);
        }

        [Theory(DisplayName = "ObterBoletimDocumento")]
        [ClassData(typeof(BoletimDocumentoGerador))]
        [Trait("Category", "BoletimDocumentoDto Scopes")]
        void
            BoletimDocumentoDto_ComSerieValida_QueAcheDocumentos_ObterBoletimDocumento_DeveRetornar_BoletimDocumentoValueObjectPreenchidoCorretamente(
                string serie, BoletimDocumento boletimDocumentoEsperado)
        {
            //Arrange
            var boletimDocumentoDto = new BoletimDocumentoDto()
            {
                Serie = serie
            };

            var documentoEntradaRepositoryMock = new Mock<IDocumentoEntradaRepository>();

            var documentoEntrada = (DocumentoEntrada)new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(boletimDocumentoEsperado.DocumentoEntradaId);

            documentoEntradaRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => documentoEntrada);

            var instrucaoServicoRepositoryMock = new Mock<IInstrucaoServicoRepository>();

            var instrucaoServico = (InstrucaoServico)new InstrucaoServicoBuilder()
                .ComInstrucaoServicoId(boletimDocumentoEsperado.InstrucaoServicoId);

            instrucaoServicoRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => instrucaoServico);


            var transferenciaRepositoryMock = new Mock<ITransferenciaRepository>();

            var transferencia = (Transferencia)new TransferenciaBuilder()
                .ComTransferenciaId(boletimDocumentoEsperado.TransferenciaId);

            transferenciaRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => transferencia);


            var ordemCarregamentoRepositoryMock = new Mock<IOrdemCarregamentoRepository>();

            var ordemCarregamento = (OrdemCarregamento)new OrdemCarregamentoBuilder()
                .ComOrdemCarregamentoId(boletimDocumentoEsperado.OrdemCarregamentoId);

            ordemCarregamentoRepositoryMock.Setup(x => x.ObterPorNumero(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => ordemCarregamento);

            //Act
            var boletimDocumento = boletimDocumentoDto.ObterBoletimDocumento(documentoEntradaRepositoryMock.Object,
                instrucaoServicoRepositoryMock.Object, transferenciaRepositoryMock.Object,
                ordemCarregamentoRepositoryMock.Object);

            //Assert
            Assert.Equal(boletimDocumentoEsperado, boletimDocumento);
        }
    }
}