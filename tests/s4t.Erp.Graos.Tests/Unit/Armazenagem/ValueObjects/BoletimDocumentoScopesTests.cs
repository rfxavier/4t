using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects
{
    public class BoletimDocumentoScopesTests
    {
        private class BoletimDocumentoInvalidoGerador : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Nde)
                        .ComDocumentoEntrada(new DocumentoEntradaBuilder()
                            .ComDocumentoEntradaId(Guid.Empty))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Nde)
                        .ComDocumentoEntrada(null)
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Is)
                        .ComInstrucaoServico(new InstrucaoServicoBuilder()
                            .ComInstrucaoServicoId(Guid.Empty))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Is)
                        .ComInstrucaoServico(null)
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Tis)
                        .ComTransferencia(new TransferenciaBuilder()
                            .ComTransferenciaId(Guid.Empty))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Tis)
                        .ComTransferencia(null)
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Tr)
                        .ComTransferencia(new TransferenciaBuilder()
                            .ComTransferenciaId(Guid.Empty))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Tr)
                        .ComTransferencia(null)
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Re)
                        .ComRemocaoNumero(0)
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Oc)
                        .ComOrdemCarregamento(new OrdemCarregamentoBuilder()
                            .ComOrdemCarregamentoId(Guid.Empty))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Oc)
                        .ComOrdemCarregamento(null)
                }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class BoletimDocumentoValidoGerador : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Nde)
                        .ComDocumentoEntrada(new DocumentoEntradaBuilder()
                            .ComDocumentoEntradaId(Guid.NewGuid()))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Is)
                        .ComInstrucaoServico(new InstrucaoServicoBuilder()
                            .ComInstrucaoServicoId(Guid.NewGuid()))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Tis)
                        .ComTransferencia(new TransferenciaBuilder()
                            .ComTransferenciaId(Guid.NewGuid()))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Tr)
                        .ComTransferencia(new TransferenciaBuilder()
                            .ComTransferenciaId(Guid.NewGuid()))
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Re)
                        .ComRemocaoNumero(1)
                },
                new object[]
                {
                    (BoletimDocumento) new BoletimDocumentoBuilder()
                        .ComSerie(BoletimSerie.Oc)
                        .ComOrdemCarregamento(new OrdemCarregamentoBuilder()
                            .ComOrdemCarregamentoId(Guid.NewGuid()))
                }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public BoletimDocumentoScopesTests()
        {
            DomainEvent.ClearCallbacks();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "PossuiSerieValida")]
        [Trait("Category", "BoletimDocumento Value Object Scopes")]
        void BoletimDocumento_ComSerieInvalida_PossuiSerieValida_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var boletimDocumentoInvalido = (BoletimDocumento) new BoletimDocumentoBuilder()
                .ComSerie(null);

            //Act

            //Assert
            Assert.False(boletimDocumentoInvalido.PossuiSerieValida());
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Série do documento é inválida");
        }

        [Theory(DisplayName = "EstaValido")]
        [ClassData(typeof(BoletimDocumentoInvalidoGerador))]
        [Trait("Category", "BoletimDocumento Value Object Scopes")]
        void BoletimDocumento_ComSerieValidaEDocumentoInvalido_EstaValido_DeveRetornarTodosOsErros(
            BoletimDocumento boletimDocumentoInvalido)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            //Act

            //Assert
            Assert.False(boletimDocumentoInvalido.EstaValido());
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento do boletim está inválido");
        }

        [Theory(DisplayName = "EstaValido")]
        [ClassData(typeof(BoletimDocumentoValidoGerador))]
        [Trait("Category", "BoletimDocumento Value Object Scopes")]
        void BoletimDocumentoValido_EstaValido_DeveRetornarSucesso(
            BoletimDocumento boletimDocumentoValido)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            //Act

            //Assert
            Assert.True(boletimDocumentoValido.EstaValido());
            Assert.Equal(0, _notifications.Count);
        }

    }
}