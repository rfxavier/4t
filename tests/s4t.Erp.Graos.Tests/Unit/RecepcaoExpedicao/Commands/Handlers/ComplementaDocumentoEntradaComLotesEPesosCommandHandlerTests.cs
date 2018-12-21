using AutoMoq;
using Moq;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Balanca.Interfaces;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Handlers;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using s4t.Erp.Graos.Tests.Unit.Balanca.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Commands.Handlers
{
    public class ComplementaDocumentoEntradaComLotesEPesosCommandHandlerTests
    {
        private readonly AutoMoqer _mocker;

        public ComplementaDocumentoEntradaComLotesEPesosCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "ComplementaDocumentoEntradaComLotesEPesosCommand Handle")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommandHandler")]
        void
            ComplementaDocumentoEntradaComLotesEPesosCommand_ComListaDeLotesNaoInformada_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            ComplementaDocumentoEntradaComLotesEPesosCommand commandInvalido =
                new ComplementaDocumentoEntradaComLotesEPesosCommand()
                {
                    Lotes = null
                };

            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Lista de Lotes não informada");
        }

        [Fact(DisplayName = "ComplementaDocumentoEntradaComLotesEPesosCommand Handle")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommandHandler")]
        void
            ComplementaDocumentoEntradaComLotesEPesosCommand_ComListaDeLotesInformadaEComPropriedadesQueNaoBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            ComplementaDocumentoEntradaComLotesEPesosCommand commandInvalido =
                new ComplementaDocumentoEntradaComLotesEPesosCommand()
                {
                    Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                    {
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = String.Empty,
                            Sacas = 0,
                            Peso = 0
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = null,
                            Sacas = -10,
                            Peso = -100
                        },

                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = "1234/A",
                            Sacas = 10,
                            Peso = 600
                        }
                    }
                };

            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(3, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Existem Lotes com número não informado");
            Assert.Contains(_notifications, e => e.Value == "Existem Lotes com sacas inválidas");
            Assert.Contains(_notifications, e => e.Value == "Existem Lotes com peso inválido");
        }

        [Fact(DisplayName = "ComplementaDocumentoEntradaComLotesEPesosCommand Handle")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommandHandler")]
        void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommand_ComDocumentoEntradaNaoInformado_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            ComplementaDocumentoEntradaComLotesEPesosCommand commandInvalido =
                new ComplementaDocumentoEntradaComLotesEPesosCommand()
                {
                    Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                    {
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = "1234/A",
                            Sacas = 1,
                            Peso = 60.5
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = "1234/B",
                            Sacas = 5,
                            Peso = 300
                        }
                    }
                };

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => null);


            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento de Entrada não informado");

            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IEmbalagemRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Never);
            _mocker.GetMock<ITicketPesagemMovimentacaoRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Never);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Update(It.IsAny<DocumentoEntrada>()), Times.Never);
        }

        [Fact(DisplayName = "ComplementaDocumentoEntradaComLotesEPesosCommand Handle")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommandHandler")]
        void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommand_ComDocumentoEntradaInformadoEComStatusDiferenteDeAberto_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            ComplementaDocumentoEntradaComLotesEPesosCommand commandInvalido =
                new ComplementaDocumentoEntradaComLotesEPesosCommand()
                {
                    Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                    {
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = "1234/A",
                            Sacas = 1,
                            Peso = 60.5
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = "1234/B",
                            Sacas = 5,
                            Peso = 300
                        }
                    }
                };

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() =>
                {
                    var documentoEntrada = (DocumentoEntrada) new DocumentoEntradaBuilder()
                        .ComDocumentoEntradaId(Guid.NewGuid());

                    documentoEntrada.TrocaStatusParaComplementadoComLotesEPesos();

                    return documentoEntrada;
                });


            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento de Entrada não está no status Em Aberto");

            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IEmbalagemRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Never);
            _mocker.GetMock<ITicketPesagemMovimentacaoRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Never);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Update(It.IsAny<DocumentoEntrada>()), Times.Never);
        }


        [Fact(DisplayName = "ComplementaDocumentoEntradaComLotesEPesosCommand Handle")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommandHandler")]
        void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommand_Invalido_ComAlgumLoteComEmbalagemNaoEncontrada_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var documentoEntrada = (DocumentoEntrada)new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid());

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => documentoEntrada);

            var embalagemId1 = Guid.NewGuid();
            var embalagemId2 = Guid.NewGuid();

            var embalagens = new List<Embalagem>()
            {
                new EmbalagemBuilder()
                    .ComEmbalagemId(embalagemId1),
                new EmbalagemBuilder()
                    .ComEmbalagemId(embalagemId2)
            };

            _mocker.GetMock<IEmbalagemRepository>()
                .Setup(x => x.ObterPorListaDeIds(It.IsAny<string>()))
                .Returns(() => embalagens);

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";
            var lote4 = "1234/D";

            ComplementaDocumentoEntradaComLotesEPesosCommand commandInvalido =
                new ComplementaDocumentoEntradaComLotesEPesosCommand()
                {
                    Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                    {
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote1,
                            Sacas = 1,
                            Peso = 60.5,
                            EmbalagemId = embalagemId1
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote2,
                            Sacas = 5,
                            Peso = 300,
                            EmbalagemId = Guid.NewGuid()
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote3,
                            Sacas = 15,
                            Peso = 320,
                            EmbalagemId = Guid.NewGuid()
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote4,
                            Sacas = 12,
                            Peso = 210,
                            EmbalagemId = Guid.Empty
                        }
                    }
                };


            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == $"Embalagem do Lote {lote2} não encontrada");
            Assert.Contains(_notifications, e => e.Value == $"Embalagem do Lote {lote3} não encontrada");

            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IEmbalagemRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Once);
            _mocker.GetMock<ITicketPesagemMovimentacaoRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Never);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Update(It.IsAny<DocumentoEntrada>()), Times.Never);
        }

        [Fact(DisplayName = "ComplementaDocumentoEntradaComLotesEPesosCommand Handle")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommandHandler")]
        void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommand_Invalido_ComAlgumLoteComTicketPesagemMovimentacaoNaoEncontrado_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var documentoEntrada = (DocumentoEntrada) new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid());

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => documentoEntrada);

            var ticketPesagemMovimentacaoId1 = Guid.NewGuid();
            var ticketPesagemMovimentacaoId2 = Guid.NewGuid();

            var ticketPesagemMovimentacoes = new List<TicketPesagemMovimentacao>()
            {
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketPesagemMovimentacaoId1),
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketPesagemMovimentacaoId2)
            };

            _mocker.GetMock<ITicketPesagemMovimentacaoRepository>()
                .Setup(x => x.ObterPorListaDeIds(It.IsAny<string>()))
                .Returns(() => ticketPesagemMovimentacoes);

            var lote1 = "1234/A";
            var lote2 = "1234/B";
            var lote3 = "1234/C";
            var lote4 = "1234/D";

            ComplementaDocumentoEntradaComLotesEPesosCommand commandInvalido =
                new ComplementaDocumentoEntradaComLotesEPesosCommand()
                {
                    Lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
                    {
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote1,
                            Sacas = 1,
                            Peso = 60.5,
                            TicketPesagemMovimentacaoId = Guid.NewGuid()
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote2,
                            Sacas = 5,
                            Peso = 300,
                            TicketPesagemMovimentacaoId = ticketPesagemMovimentacaoId1
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote3,
                            Sacas = 15,
                            Peso = 320,
                            TicketPesagemMovimentacaoId = Guid.Empty
                        },
                        new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                        {
                            Numero = lote4,
                            Sacas = 12,
                            Peso = 210,
                            TicketPesagemMovimentacaoId = Guid.NewGuid()
                        }
                    }
                };


            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == $"Ticket Pesagem Movimentação do Lote {lote1} não encontrado");
            Assert.Contains(_notifications, e => e.Value == $"Ticket Pesagem Movimentação do Lote {lote4} não encontrado");

            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IEmbalagemRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Never);
            _mocker.GetMock<ITicketPesagemMovimentacaoRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Once);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Update(It.IsAny<DocumentoEntrada>()), Times.Never);
        }

        [Fact(DisplayName = "ComplementaDocumentoEntradaComLotesEPesosCommand Handle")]
        [Trait("Category", "ComplementaDocumentoEntradaComLotesEPesosCommandHandler")]
        void
            ComplementaDocumentoEntradaComLotesEPesosLoteCommand_Valido_Handle_DeveRetornarSucesso_SeAdicionouLotesCorretamente()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var filialId = Guid.NewGuid();

            var destinatarioCadastroId = Guid.NewGuid();
            var destinatarioFazendaId = Guid.NewGuid();

            var notaFiscalGraos = new NotaFiscalGraos()
            {
                TipoGrao = 1,
                DestinatarioCadastroId = destinatarioCadastroId,
                DestinatarioFazendaId = destinatarioFazendaId
            };

            var documentoEntrada = (DocumentoEntrada)new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid())
                .ComFilialId(filialId)
                .ComNotaFiscalGraos(notaFiscalGraos);

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => documentoEntrada);

            var embalagemId1 = Guid.NewGuid();
            var embalagemId2 = Guid.NewGuid();

            var embalagens = new List<Embalagem>()
            {
                new EmbalagemBuilder()
                    .ComEmbalagemId(embalagemId1),
                new EmbalagemBuilder()
                    .ComEmbalagemId(embalagemId2)
            };

            _mocker.GetMock<IEmbalagemRepository>()
                .Setup(x => x.ObterPorListaDeIds(It.IsAny<string>()))
                .Returns(() => embalagens);


            var ticketPesagemMovimentacaoId1 = Guid.NewGuid();
            var ticketPesagemMovimentacaoId2 = Guid.NewGuid();

            var ticketPesagemMovimentacoes = new List<TicketPesagemMovimentacao>()
            {
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketPesagemMovimentacaoId1),
                new TicketPesagemMovimentacaoBuilder()
                    .ComTicketPesagemMovimentacaoId(ticketPesagemMovimentacaoId2)
            };

            _mocker.GetMock<ITicketPesagemMovimentacaoRepository>()
                .Setup(x => x.ObterPorListaDeIds(It.IsAny<string>()))
                .Returns(() => ticketPesagemMovimentacoes);

            var lote1 = "1234/A";
            var lote2 = "1234/B";

            var lotes = new List<ComplementaDocumentoEntradaComLotesEPesosLoteCommand>()
            {
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote1,
                    Sacas = 1,
                    Peso = 60.5,
                    EmbalagemId = embalagemId1,
                    TicketPesagemMovimentacaoId = ticketPesagemMovimentacaoId1
                },
                new ComplementaDocumentoEntradaComLotesEPesosLoteCommand()
                {
                    Numero = lote2,
                    Sacas = 5,
                    Peso = 300,
                    EmbalagemId = embalagemId2,
                    TicketPesagemMovimentacaoId = ticketPesagemMovimentacaoId2
                }
            };

            var commandValido = new ComplementaDocumentoEntradaComLotesEPesosCommand()
            {
                DocumentoEntradaId = documentoEntrada.Id,
                Lotes = lotes
            };

            DocumentoEntrada documentoEntradaParaUpdate = null;

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.Update(It.IsAny<DocumentoEntrada>()))
                .Callback((DocumentoEntrada d) =>
                {
                    documentoEntradaParaUpdate = d;
                })
                .Returns(() => documentoEntradaParaUpdate);

            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            var lotesDocumentoEntrada = (IList<Lote>) documentoEntrada.Lotes;

            Assert.Equal(0, _notifications.Count);
            Assert.True(documentoEntrada.Lotes.Count == lotes.Count);
            Assert.True(documentoEntrada.Lotes.All(d => d.Id != Guid.Empty));
            Assert.True(documentoEntrada.Lotes.All(d => d.FilialId == filialId));

            for (int i = 0; i < documentoEntrada.Lotes.Count - 1; i++)
            {
                Assert.True(lotesDocumentoEntrada[i].Numero == lotes[i].Numero);
                Assert.True(lotesDocumentoEntrada[i].Sacas == lotes[i].Sacas);
                Assert.True(lotesDocumentoEntrada[i].Peso == lotes[i].Peso);
                Assert.True(lotesDocumentoEntrada[i].TipoGrao.Value == notaFiscalGraos.TipoGrao);
                Assert.True(lotesDocumentoEntrada[i].Embalagem.Id == lotes[i].EmbalagemId);
                Assert.True(lotesDocumentoEntrada[i].TicketPesagemMovimentacao.Id == lotes[i].TicketPesagemMovimentacaoId);
                Assert.True(lotesDocumentoEntrada[i].CadastroTitularId == notaFiscalGraos.DestinatarioCadastroId);
                Assert.True(lotesDocumentoEntrada[i].FazendaTitularId == notaFiscalGraos.DestinatarioFazendaId);
                Assert.True(lotesDocumentoEntrada[i].DocumentoEntrada == documentoEntradaParaUpdate);
            }

            Assert.True(documentoEntrada.DocumentoEntradaStatus == DocumentoEntradaStatus.ComplementadoComLotesEPesos);

            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IEmbalagemRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Once);
            _mocker.GetMock<ITicketPesagemMovimentacaoRepository>().Verify(x => x.ObterPorListaDeIds(It.IsAny<string>()), Times.Once);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Update(It.IsAny<DocumentoEntrada>()), Times.Once);
        }
    }
}