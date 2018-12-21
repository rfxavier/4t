using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Tests.Unit.Balanca.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Commands.Scopes
{
    public class CommandScopesTests
    {
        private class DocumentoEntradaInvalidoGerador : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {null},
                new object[] {(DocumentoEntrada) new DocumentoEntradaBuilder().ComDocumentoEntradaId(Guid.Empty)}
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class ListaDeLoteInvalidaGerador : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {null},
                new object[] {new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()}
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class ListaDeGuidGerador : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {new List<Guid>() {Guid.Empty} },
                new object[] {new List<Guid>() {Guid.NewGuid()} },
                new object[] {new List<Guid>() {Guid.NewGuid(), Guid.NewGuid()}  }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public CommandScopesTests()
        {
            DomainEvent.ClearCallbacks();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Theory(DisplayName = "PossuiDocumentoEntradaInformado")]
        [ClassData(typeof(DocumentoEntradaInvalidoGerador))]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_SemDocumentoEntradaInformado_PossuiDocumentoEntradaInformado_DeveRetornarTodosOsErros(
                DocumentoEntrada documentoEntrada)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand =
                new ComplementaDocumentoEntradaComLotesEPesosCommand();

            //Act
            var resultado =
                complementaDocumentoEntradaComLotesEPesosCommand.PossuiDocumentoEntradaInformado(documentoEntrada);

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento de Entrada não informado");
        }

        [Fact(DisplayName = "PossuiDocumentoEntradaInformado")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComDocumentoEntradaInformado_PossuiDocumentoEntradaInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand =
                new ComplementaDocumentoEntradaComLotesEPesosCommand();

            var documentoEntrada = new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid());

            //Act
            var resultado =
                complementaDocumentoEntradaComLotesEPesosCommand.PossuiDocumentoEntradaInformado(documentoEntrada);

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Theory(DisplayName = "PossuiListaDeLotesInformada")]
        [ClassData(typeof(ListaDeLoteInvalidaGerador))]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_SemListaDeLotesInformada_PossuiListaDeLotesInformada_DeveRetornarTodosOsErros(
                List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand> lotes)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = lotes
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiListaDeLotesInformada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Lista de Lotes não informada");
        }

        [Fact(DisplayName = "PossuiListaDeLotesInformada")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesInformada_PossuiListaDeLotesInformada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand(),
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiListaDeLotesInformada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiTodosLotesComNumeroInformado")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComAlgumNumeroNaoInformado_PossuiTodosLotesComNumeroInformado_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Numero = string.Empty
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Numero = "1234"
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiTodosLotesComNumeroInformado();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Existem Lotes com número não informado");

        }

        [Fact(DisplayName = "PossuiTodosLotesComNumeroInformado")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComTodosNumerosInformados_PossuiTodosLotesComNumeroInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Numero = "4567"
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Numero = "1234"
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiTodosLotesComNumeroInformado();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiTodosLotesComSacasMaiorQueZero")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComAlgumNumeroNaoInformado_PossuiTodosLotesComSacasMaiorQueZero_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Sacas = 0
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Sacas = 10
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Sacas = -15
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiTodosLotesComSacasMaiorQueZero();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Existem Lotes com sacas inválidas");

        }

        [Fact(DisplayName = "PossuiTodosLotesComSacasMaiorQueZero")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComTodosNumerosInformados_PossuiTodosLotesComSacasMaiorQueZero_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Sacas = 20
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Sacas = 1
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiTodosLotesComSacasMaiorQueZero();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiTodosLotesComPesoMaiorQueZero")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComAlgumNumeroNaoInformado_PossuiTodosLotesComPesoMaiorQueZero_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Peso = 0
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Peso = 0.1
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Peso = -900
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiTodosLotesComPesoMaiorQueZero();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Existem Lotes com peso inválido");

        }

        [Fact(DisplayName = "PossuiTodosLotesComPesoMaiorQueZero")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComTodosNumerosInformados_PossuiTodosLotesComPesoMaiorQueZero_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Peso = 1200
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        Peso = 60.5
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiTodosLotesComPesoMaiorQueZero();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiAlgumLoteComEmbalagemInformada")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComNenhumaEmbalagemInformada_PossuiAlgumLoteComEmbalagemInformada_DeveRetornarFalse()
        {
            //Arrange
            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        EmbalagemId = Guid.Empty
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        EmbalagemId = Guid.Empty
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiAlgumLoteComEmbalagemInformada();

            //Assert
            Assert.False(resultado);
        }

        [Fact(DisplayName = "PossuiAlgumLoteComEmbalagemInformada")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComPeloMenosUmaEmbalagemInformada_PossuiAlgumLoteComEmbalagemInformada_DeveRetornarTrue()
        {
            //Arrange
            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        EmbalagemId = Guid.Empty
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        EmbalagemId = Guid.NewGuid()
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiAlgumLoteComEmbalagemInformada();

            //Assert
            Assert.True(resultado);
        }

        [Theory(DisplayName = "ListaDeEmbalagemIdsToString")]
        [ClassData(typeof(ListaDeGuidGerador))]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ListaDeEmbalagemIdsToString_DeveRetornarStringCorreta(List<Guid> guids)
        {
            //Arrange
            var lotes = guids.Select(g => new ComplementaDocumentoEntradaComLotesEPesosLoteCommand() { EmbalagemId = g }).ToList();

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = lotes
            };

            var resultadoEsperado = "'" + string.Join("','", guids) + "'";

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.ListaDeEmbalagemIdsToString();

            //Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Fact(DisplayName = "PossuiAlgumLoteComTicketPesagemMovimentacaoInformado")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComNenhumTicketPesagemMovimentacaoInformado_PossuiAlgumLoteComTicketPesagemMovimentacaoInformado_DeveRetornarFalse()
        {
            //Arrange
            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        TicketPesagemMovimentacaoId = Guid.Empty
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        TicketPesagemMovimentacaoId = Guid.Empty
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiAlgumLoteComTicketPesagemMovimentacaoInformado();

            //Assert
            Assert.False(resultado);
        }

        [Fact(DisplayName = "PossuiAlgumLoteComTicketPesagemMovimentacaoInformado")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ComListaDeLotesComPeloMenosUmTicketPesagemMovimentacaoInformado_PossuiAlgumLoteComTicketPesagemMovimentacaoInformado_DeveRetornarTrue()
        {
            //Arrange
            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                {
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        TicketPesagemMovimentacaoId = Guid.Empty
                    },
                    new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                    {
                        TicketPesagemMovimentacaoId = Guid.NewGuid()
                    }
                }
            };

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.PossuiAlgumLoteComTicketPesagemMovimentacaoInformado();

            //Assert
            Assert.True(resultado);
        }

        [Theory(DisplayName = "ListaDeTicketPesagemMovimentacaoIdsToString")]
        [ClassData(typeof(ListaDeGuidGerador))]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosCommandScope_ListaDeTicketPesagemMovimentacaoIdsToString_DeveRetornarStringCorreta(List<Guid> guids)
        {
            //Arrange
            var lotes = guids.Select(g => new ComplementaDocumentoEntradaComLotesEPesosLoteCommand() { TicketPesagemMovimentacaoId = g }).ToList();

            var complementaDocumentoEntradaComLotesEPesosCommand = new ComplementaDocumentoEntradaComLotesEPesosCommand
            {
                Lotes = lotes
            };

            var resultadoEsperado = "'" + string.Join("','", guids) + "'";

            //Act
            var resultado = complementaDocumentoEntradaComLotesEPesosCommand.ListaDeTicketPesagemMovimentacaoIdsToString();

            //Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Fact(DisplayName = "PossuiEmbalagemOpcionalInformada")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosLoteCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommandScope_ComPeloMenosUmaEmbalagemNaoInformada_PossuiEmbalagemOpcionalInformada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";

            var embalagemId1 = Guid.NewGuid();
            var embalagemId2 = Guid.NewGuid();

            var embalagens = new List<Embalagem>
            {
                new EmbalagemBuilder()
                .ComEmbalagemId(embalagemId1),
                new EmbalagemBuilder()
                .ComEmbalagemId(embalagemId2)
            };

            var lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
            {
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote1,
                    EmbalagemId = Guid.NewGuid()
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote2,
                    EmbalagemId = embalagemId2
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote3,
                    EmbalagemId = Guid.Empty
                }
            };

            var lotesOk = true;

            //Act
            foreach (var lote in lotes)
            {
                lotesOk = lotesOk & lote.PossuiEmbalagemOpcionalInformada(embalagens);
            }

            //Assert
            Assert.False(lotesOk);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == $"Embalagem do Lote {lote1} não encontrada");
            //Assert.Contains(_notifications, e => e.Value == $"Embalagem do Lote {lote3} não encontrada");
        }

        [Fact(DisplayName = "PossuiEmbalagemOpcionalInformada")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosLoteCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommandScope_ComTodasEmbalagensInformadas_PossuiEmbalagemInformada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";

            var embalagemId1 = Guid.NewGuid();
            var embalagemId2 = Guid.NewGuid();

            var embalagens = new List<Embalagem>
            {
                new EmbalagemBuilder()
                .ComEmbalagemId(embalagemId1),
                new EmbalagemBuilder()
                .ComEmbalagemId(embalagemId2)
            };

            var lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
            {
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote1,
                    EmbalagemId = embalagemId1
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote2,
                    EmbalagemId = embalagemId2
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote3,
                    EmbalagemId = embalagemId1
                }
            };

            var lotesOk = true;

            //Act
            foreach (var lote in lotes)
            {
                lotesOk = lotesOk & lote.PossuiEmbalagemOpcionalInformada(embalagens);
            }

            //Assert
            Assert.True(lotesOk);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiEmbalagemQuantidadeMaiorQueZero")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosLoteCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommandScope_ComPeloMenosUmaEmbalagemComQuantidadeInvalida_PossuiEmbalagemQuantidadeMaiorQueZero_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";

            var lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
            {
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote1,
                    QuantidadeEmbalagem = 1
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote2,
                    QuantidadeEmbalagem = 0
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote3,
                    QuantidadeEmbalagem = -1
                }
            };

            var lotesOk = true;

            //Act
            foreach (var lote in lotes)
            {
                lotesOk = lotesOk & lote.PossuiEmbalagemQuantidadeMaiorQueZero();
            }

            //Assert
            Assert.False(lotesOk);
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == $"Embalagem do Lote {lote2} tem quantidade inválida");
            Assert.Contains(_notifications, e => e.Value == $"Embalagem do Lote {lote3} tem quantidade inválida");
        }

        [Fact(DisplayName = "PossuiEmbalagemQuantidadeMaiorQueZero")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosLoteCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommandScope_ComTodasEmbalagensComQuantidadeMaiorQueZero_PossuiEmbalagemQuantidadeMaiorQueZero_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";

            var lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
            {
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote1,
                    QuantidadeEmbalagem = 1
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote2,
                    QuantidadeEmbalagem = 3
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote3,
                    QuantidadeEmbalagem = 5
                }
            };

            var lotesOk = true;

            //Act
            foreach (var lote in lotes)
            {
                lotesOk = lotesOk & lote.PossuiEmbalagemQuantidadeMaiorQueZero();
            }

            //Assert
            Assert.True(lotesOk);
            Assert.Equal(0, _notifications.Count);
        }


        [Fact(DisplayName = "PossuiTicketPesagemMovimentacaoOpcionalInformado")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosLoteCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommandScope_ComPeloMenosUmTicketMovimentacaoNaoInformado_PossuiTicketPesagemMovimentacaoInformado_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";
            var lote4 = "1234/D";

            var ticketMovimentacaoId1 = Guid.NewGuid();
            var ticketMovimentacaoId2 = Guid.NewGuid();

            var ticketMovimentacoes = new List<TicketPesagemMovimentacao>
            {
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketMovimentacaoId1),
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketMovimentacaoId2)
            };

            var lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
            {
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote1,
                    TicketPesagemMovimentacaoId = Guid.NewGuid()
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote2,
                    TicketPesagemMovimentacaoId = ticketMovimentacaoId2
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote3,
                    TicketPesagemMovimentacaoId = Guid.NewGuid()
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote4,
                    TicketPesagemMovimentacaoId = Guid.Empty
                }
            };

            var lotesOk = true;

            //Act
            foreach (var lote in lotes)
            {
                lotesOk = lotesOk & lote.PossuiTicketPesagemMovimentacaoOpcionalInformado(ticketMovimentacoes);
            }

            //Assert
            Assert.False(lotesOk);
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == $"Ticket Pesagem Movimentação do Lote {lote1} não encontrado");
            Assert.Contains(_notifications, e => e.Value == $"Ticket Pesagem Movimentação do Lote {lote3} não encontrado");
        }

        [Fact(DisplayName = "PossuiTicketPesagemMovimentacaoOpcionalInformado")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosLoteCommand Scopes")]
        public void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommandScope_ComTodosTicketsMovimentacaoInformados_PossuiTicketPesagemMovimentacaoInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";

            var ticketMovimentacaoId1 = Guid.NewGuid();
            var ticketMovimentacaoId2 = Guid.NewGuid();

            var ticketMovimentacoes = new List<TicketPesagemMovimentacao>
            {
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketMovimentacaoId1),
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketMovimentacaoId2)
            };

            var lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
            {
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote1,
                    TicketPesagemMovimentacaoId = ticketMovimentacaoId1
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote2,
                    TicketPesagemMovimentacaoId = ticketMovimentacaoId2
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote3,
                    TicketPesagemMovimentacaoId = ticketMovimentacaoId1
                }
            };

            var lotesOk = true;

            //Act
            foreach (var lote in lotes)
            {
                lotesOk = lotesOk & lote.PossuiTicketPesagemMovimentacaoOpcionalInformado(ticketMovimentacoes);
            }

            //Assert
            Assert.True(lotesOk);
            Assert.Equal(0, _notifications.Count);
        }

    }
}