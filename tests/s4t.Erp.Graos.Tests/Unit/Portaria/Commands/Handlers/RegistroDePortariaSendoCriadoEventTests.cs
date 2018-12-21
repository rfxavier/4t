using AutoMoq;
using Moq;
using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Core.Domain.ValueObjects;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Domain.Portaria.Commands.Handlers;
using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Enums;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEventHandlers;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;
using s4t.Erp.Graos.Tests.Unit.Portaria.Entities.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Handlers
{
    public class RegistroDePortariaSendoCriadoEventTests
    {
        private readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();
        private RegistroDePortariaSendoCriadoEvent _eventRegistroDePortaria;
        private RegistroDePortariaServicoAvulsoSendoCriadoEvent _eventRegistroDePortariaServicoAvulso;

        public RegistroDePortariaSendoCriadoEventTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle Event")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_Valido_Handle_DeveGerarRegistroDePortariaLog_AtravesDeEvento()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            RegistroDePortariaSendoCriadoHandlerParaLog handlerRegistroDePortariaSendoCriadoEvent = null;

            DomainEvent.Register<RegistroDePortariaSendoCriadoEvent>(ev =>
            {
                _eventRegistroDePortaria = ev;
                handlerRegistroDePortariaSendoCriadoEvent.Handle(_eventRegistroDePortaria);
            });

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido = GeraTicketDesembarqueParaDepositoValidoCommand();

            TicketPortaria ticketPortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new Motorista(Guid.NewGuid(), 1, "Motorista 1", new CPF("95004890668")));

            _mocker.GetMock<INotaFiscalRepository>().Setup(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => new List<NotaFiscalGraos>
                {
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.EntradaParaDeposito.Value
                    },
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.EntradaParaDeposito.Value
                    }
                });

            _mocker.GetMock<IRegistroDePortariaRepository>().Setup(x => x.Add(It.IsAny<RegistroDePortaria>()));

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Callback((TicketPortaria t) => { ticketPortaria = new TicketPortariaBuilder()
                    .ComTicketPortariaId(t.Id)
                    .ComNumero(t.Numero)
                    .ComRegistroDePortaria(t.RegistroDePortaria); })
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handlerGeraTicketCommand = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            RegistroDePortariaLog registroDePortariaLog = null;

            _mocker.GetMock<IRegistroDePortariaLogRepository>().Setup(x => x.Add(It.IsAny<RegistroDePortariaLog>()))
                .Callback((RegistroDePortariaLog r) =>
                {
                    registroDePortariaLog = new RegistroDePortariaLog
                    {
                        RegistroDePortariaId = _eventRegistroDePortaria.RegistroDePortaria.Id,
                        FilialCodigo = _eventRegistroDePortaria.Filial.Codigo,
                        FilialNome = _eventRegistroDePortaria.Filial.Nome,
                        TipoGrao = _eventRegistroDePortaria.RegistroDePortaria.TipoGrao.Value,
                        TipoGraoDescricao = _eventRegistroDePortaria.RegistroDePortaria.TipoGrao.Name,
                        TipoOperacao = _eventRegistroDePortaria.RegistroDePortaria.TipoOperacaoPortaria.Value,
                        TipoOperacaoDescricao = _eventRegistroDePortaria.RegistroDePortaria.TipoOperacaoPortaria.Name,
                        Placa = _eventRegistroDePortaria.RegistroDePortaria.Placa.Numero,
                        MotoristaCodigo = _eventRegistroDePortaria.RegistroDePortaria.Motorista.Codigo,
                        MotoristaNome = _eventRegistroDePortaria.RegistroDePortaria.Motorista.Nome,
                        Data = _eventRegistroDePortaria.RegistroDePortaria.Data
                    };
                })
                .Returns(() => registroDePortariaLog);

            handlerRegistroDePortariaSendoCriadoEvent = _mocker.Resolve<RegistroDePortariaSendoCriadoHandlerParaLog>();

            //Act
            handlerGeraTicketCommand.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(_eventRegistroDePortaria != null && _eventRegistroDePortaria.RegistroDePortaria != null && _eventRegistroDePortaria.RegistroDePortaria.Id != Guid.Empty);
            Assert.NotNull(registroDePortariaLog);
            Assert.True(registroDePortariaLog.RegistroDePortariaId != Guid.Empty && registroDePortariaLog.RegistroDePortariaServicoAvulsoId == Guid.Empty);

            _mocker.GetMock<IRegistroDePortariaLogRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortariaLog>()), Times.Once);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandHandler Handle Event")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand_Valido_Handle_DeveGerarRegistroDePortariaLog_AtravesDeEvento()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            RegistroDePortariaSendoCriadoHandlerParaLog handlerRegistroDePortariaSendoCriadoEvent = null;

            DomainEvent.Register<RegistroDePortariaSendoCriadoEvent>(ev => {
                _eventRegistroDePortaria = ev;
                handlerRegistroDePortariaSendoCriadoEvent.Handle(_eventRegistroDePortaria);
            });

            GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand commandValido = GeraTicketDesembarqueParaTransferenciaValidoCommand();

            TicketPortaria ticketPortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new Motorista(Guid.NewGuid(), 1, "Motorista 1", new CPF("95004890668")));

            _mocker.GetMock<INotaFiscalRepository>().Setup(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => new List<NotaFiscalGraos>
                {
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.SaidaParaTransferencia.Value
                    },
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.SaidaParaTransferencia.Value
                    }
                });

            _mocker.GetMock<IRegistroDePortariaRepository>().Setup(x => x.Add(It.IsAny<RegistroDePortaria>()));

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Callback((TicketPortaria t) => {
                    ticketPortaria = new TicketPortariaBuilder()
                        .ComTicketPortariaId(t.Id)
                        .ComNumero(t.Numero)
                        .ComRegistroDePortaria(t.RegistroDePortaria);
                })
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handlerGeraTicketCommand = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            RegistroDePortariaLog registroDePortariaLog = null;

            _mocker.GetMock<IRegistroDePortariaLogRepository>().Setup(x => x.Add(It.IsAny<RegistroDePortariaLog>()))
                .Callback((RegistroDePortariaLog r) =>
                {
                    registroDePortariaLog = new RegistroDePortariaLog
                    {
                        RegistroDePortariaId = _eventRegistroDePortaria.RegistroDePortaria.Id,
                        FilialCodigo = _eventRegistroDePortaria.Filial.Codigo,
                        FilialNome = _eventRegistroDePortaria.Filial.Nome,
                        TipoGrao = _eventRegistroDePortaria.RegistroDePortaria.TipoGrao.Value,
                        TipoGraoDescricao = _eventRegistroDePortaria.RegistroDePortaria.TipoGrao.Name,
                        TipoOperacao = _eventRegistroDePortaria.RegistroDePortaria.TipoOperacaoPortaria.Value,
                        TipoOperacaoDescricao = _eventRegistroDePortaria.RegistroDePortaria.TipoOperacaoPortaria.Name,
                        Placa = _eventRegistroDePortaria.RegistroDePortaria.Placa.Numero,
                        MotoristaCodigo = _eventRegistroDePortaria.RegistroDePortaria.Motorista.Codigo,
                        MotoristaNome = _eventRegistroDePortaria.RegistroDePortaria.Motorista.Nome,
                        Data = _eventRegistroDePortaria.RegistroDePortaria.Data
                    };
                })
                .Returns(() => registroDePortariaLog);

            handlerRegistroDePortariaSendoCriadoEvent = _mocker.Resolve<RegistroDePortariaSendoCriadoHandlerParaLog>();

            //Act
            handlerGeraTicketCommand.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(_eventRegistroDePortaria != null && _eventRegistroDePortaria.RegistroDePortaria != null && _eventRegistroDePortaria.RegistroDePortaria.Id != Guid.Empty);
            Assert.NotNull(registroDePortariaLog);
            Assert.True(registroDePortariaLog.RegistroDePortariaId != Guid.Empty && registroDePortariaLog.RegistroDePortariaServicoAvulsoId == Guid.Empty);

            _mocker.GetMock<IRegistroDePortariaLogRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortariaLog>()), Times.Once);
        }

        [Fact(DisplayName = "GeraTicketPortariaServicoAvulsoCommandHandler Handle Event")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void GeraTicketPortariaServicoAvulsoCommand_Valido_Handle_DeveGerarRegistroDePortariaLog_AtravesDeEvento()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            RegistroDePortariaServicoAvulsoSendoCriadoHandlerParaLog handlerRegistroDePortariaServicoAvulsoSendoCriadoEvent = null;

            DomainEvent.Register<RegistroDePortariaServicoAvulsoSendoCriadoEvent>(ev => {
                _eventRegistroDePortariaServicoAvulso = ev;
                handlerRegistroDePortariaServicoAvulsoSendoCriadoEvent.Handle(_eventRegistroDePortariaServicoAvulso);
            });

            GeraTicketPortariaServicoAvulsoCommand commandValido = GeraTicketServicoAvulsoValidoCommand();

            TicketPortaria ticketPortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IProdutoPortariaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new ProdutoPortaria(Guid.NewGuid(), 1, "Produto 1"));

            _mocker.GetMock<IRegistroDePortariaServicoAvulsoRepository>()
                .Setup(x => x.Add(It.IsAny<RegistroDePortariaServicoAvulso>()));

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Callback((TicketPortaria t) => {
                    ticketPortaria = new TicketPortariaBuilder()
                        .ComTicketPortariaId(t.Id)
                        .ComNumero(t.Numero)
                        .ComRegistroDePortaria(t.RegistroDePortaria);
                })
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handlerGeraTicketServicoAvulsoCommand = _mocker.Resolve<GeraTicketPortariaServicoAvulsoCommandHandler>();

            RegistroDePortariaLog registroDePortariaLog = null;

            _mocker.GetMock<IRegistroDePortariaLogRepository>().Setup(x => x.Add(It.IsAny<RegistroDePortariaLog>()))
                .Callback((RegistroDePortariaLog r) =>
                {
                    var motoristaCodigo = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Motorista == null ? 0 : _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Motorista.Codigo;
                    var motoristaNome = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Motorista == null ? "" : _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Motorista.Nome;

                    registroDePortariaLog = new RegistroDePortariaLog
                    {
                        RegistroDePortariaServicoAvulsoId = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Id,
                        FilialCodigo = _eventRegistroDePortariaServicoAvulso.Filial.Codigo,
                        FilialNome = _eventRegistroDePortariaServicoAvulso.Filial.Nome,
                        Placa = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Placa.Numero,
                        MotoristaCodigo = motoristaCodigo,
                        MotoristaNome = motoristaNome,
                        NomeDoMotoristaSemCadastro = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.NomeDoMotoristaSemCadastro,
                        ProdutoPortariaCodigo = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.ProdutoPortaria.Codigo,
                        ProdutoPortariaDescricao = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.ProdutoPortaria.Descricao,
                        Data = _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Data
                    };
                })
                .Returns(() => registroDePortariaLog);

            handlerRegistroDePortariaServicoAvulsoSendoCriadoEvent = _mocker.Resolve<RegistroDePortariaServicoAvulsoSendoCriadoHandlerParaLog>();

            //Act
            handlerGeraTicketServicoAvulsoCommand.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(_eventRegistroDePortariaServicoAvulso != null && _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso != null && _eventRegistroDePortariaServicoAvulso.RegistroDePortariaServicoAvulso.Id != Guid.Empty);
            Assert.NotNull(registroDePortariaLog);
            Assert.True(registroDePortariaLog.RegistroDePortariaId == Guid.Empty && registroDePortariaLog.RegistroDePortariaServicoAvulsoId != Guid.Empty);

            _mocker.GetMock<IRegistroDePortariaLogRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortariaLog>()), Times.Once);
        }
        private static GeraTicketPortariaDesembarqueParaEntradaDepositoCommand GeraTicketDesembarqueParaDepositoValidoCommand()
        {
            var commandValido =
                new GeraTicketPortariaDesembarqueParaEntradaDepositoCommand(Guid.NewGuid(), 1, "OPH0036", Guid.NewGuid());

            var commandNotaFiscal1 = new GeraTicketPortariaNotaFiscalCommand(Guid.NewGuid());
            var commandNotaFiscal2 = new GeraTicketPortariaNotaFiscalCommand(Guid.NewGuid());

            commandValido.NotasFiscais.Add(commandNotaFiscal1);
            commandValido.NotasFiscais.Add(commandNotaFiscal2);

            return commandValido;
        }

        private static GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand GeraTicketDesembarqueParaTransferenciaValidoCommand()
        {
            var commandValido = new GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand(Guid.NewGuid(), 1, "OPH0036", Guid.NewGuid());

            var commandNotaFiscal1 = new GeraTicketPortariaNotaFiscalCommand(Guid.NewGuid());
            var commandNotaFiscal2 = new GeraTicketPortariaNotaFiscalCommand(Guid.NewGuid());

            commandValido.NotasFiscais.Add(commandNotaFiscal1);
            commandValido.NotasFiscais.Add(commandNotaFiscal2);

            return commandValido;
        }

        private static GeraTicketPortariaServicoAvulsoCommand GeraTicketServicoAvulsoValidoCommand()
        {
            var commandValido =
                new GeraTicketPortariaServicoAvulsoCommand(Guid.NewGuid(), "OPH0036", Guid.NewGuid(), "", Guid.NewGuid());

            return commandValido;
        }
    }
}