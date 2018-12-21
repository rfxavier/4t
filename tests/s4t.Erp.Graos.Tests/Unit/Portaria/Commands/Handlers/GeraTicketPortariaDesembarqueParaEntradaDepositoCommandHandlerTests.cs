using AutoMoq;
using Moq;
using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Domain.Portaria.Commands.Handlers;
using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Enums;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEventHandlers;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;
using s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Builders;
using s4t.Erp.Graos.Tests.Unit.Portaria.Entities.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Interfaces;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Handlers
{
    public class GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandlerTests
    {
        private readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();
        private RegistroDePortariaSendoCriadoEvent _eventRegistroDePortaria;

        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_ComPropriedadesQueNaoBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var commandInvalido = new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                .ComTipoGrao(0)
                .ComPlacaNumero("");

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Tipo do grão não é valido");
            Assert.Contains(_notifications, e => e.Value == "Placa de veículo é obrigatória");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Never);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Never);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Never);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Never);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Never);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_ComPropriedadesQueBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            //Arrange
            var commandInvalido = new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                .ComTipoGrao(TipoGrao.Cafe.Value)
                .ComPlacaNumero("OPH0036")
                .ComFilialId(Guid.Empty)
                .ComMotoristaId(Guid.Empty);

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => null);

            _mocker.GetMock<IMotoristaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(3, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial não informada");
            Assert.Contains(_notifications, e => e.Value == "Motorista não informado");
            Assert.Contains(_notifications, e => e.Value == "Notas Fiscais para Desembarque Para Entrada Depósito não informadas");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Never);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Never);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Never);
        }


        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_Valido_Handle_DeveRetornarSucesso_SeGerouTicket()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido = GeraTicketDesembarqueParaDepositoValidoCommand();

            TicketPortaria ticketPortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

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
                .Callback((TicketPortaria t) =>
                {
                    ticketPortaria = new TicketPortariaBuilder()
                        .ComTicketPortariaId(t.Id)
                        .ComNumero(t.Numero)
                        .ComRegistroDePortaria(t.RegistroDePortaria);
                })
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Once);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);

            Assert.True(ticketPortaria.Id != Guid.Empty);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_Valido_Handle_DeveRetornarSucesso_SeGerouTicketETicketFoiAtreladoARegistroDePortaria()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido =
                GeraTicketDesembarqueParaDepositoValidoCommand();

            TicketPortaria ticketPortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

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

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Callback((TicketPortaria t) =>
                {
                    ticketPortaria = new TicketPortariaBuilder()
                        .ComTicketPortariaId(t.Id)
                        .ComNumero(t.Numero)
                        .ComRegistroDePortaria(t.RegistroDePortaria);
                })
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Once);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);

            Assert.True(ticketPortaria.Id != Guid.Empty);
            Assert.True(ticketPortaria.RegistroDePortaria != null);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_Valido_Handle_DeveRetornarErro_SeGerouTicketETicketNaoFoiAtreladoARegistroDePortaria()
        {
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            //Arrange
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido =
                GeraTicketDesembarqueParaDepositoValidoCommand();

            TicketPortaria ticketPortaria = new TicketPortariaBuilder()
                .ComTicketPortariaId(Guid.NewGuid())
                .ComRegistroDePortaria(null);

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

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

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Once);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);

            Assert.True(ticketPortaria.Id != Guid.Empty);
            Assert.True(ticketPortaria.RegistroDePortaria == null);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_SemNotasFiscaisInformadas_Handle_DeveRetornarErro()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandInvalidoSemNotasFiscais = GeraTicketDesembarqueParaDepositoSemNotasFiscaisCommand();

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

            _mocker.GetMock<INotaFiscalRepository>().Setup(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => null);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse(true));

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandInvalidoSemNotasFiscais);

            //Assert
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Notas Fiscais para Desembarque Para Entrada Depósito não informadas");
            Assert.Contains(_notifications, e => e.Value == "Notas Fiscais para Desembarque para Entrada Depósito não são do tipo correto");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Never);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Never);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Never);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_ComNotasFiscaisNaoIgualADeposito_Handle_DeveRetornarErro()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido =
                GeraTicketDesembarqueParaDepositoValidoCommand();

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

            _mocker.GetMock<INotaFiscalRepository>().Setup(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => new List<NotaFiscalGraos>
                {
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.EntradaParaDeposito.Value
                    },
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.SaidaParaTransferencia.Value
                    },
                });

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse(true));

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Notas Fiscais para Desembarque para Entrada Depósito não são do tipo correto");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Never);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Never);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Never);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_ComNotasFiscaisIgualADeposito_Handle_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido =
                GeraTicketDesembarqueParaDepositoValidoCommand();

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

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

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse(true));

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Once);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_ValidoComCertificacoesNasNotasFiscais_Handle_DeveAdicionarRegistroDePortaria_ComMesmasCertificacoesAtreladas()
        {
            //Arrange
            RegistroDePortariaSendoCriadoHandlerParaCertificacoes handlerCertificacoes = null;

            DomainEvent.Register<RegistroDePortariaSendoCriadoEvent>(ev => {
                _eventRegistroDePortaria = ev;
                handlerCertificacoes.Handle(_eventRegistroDePortaria);
            });

            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido = GeraTicketDesembarqueParaDepositoValidoCommand();

            RegistroDePortaria registroDePortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

            _mocker.GetMock<INotaFiscalRepository>().Setup(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => new List<NotaFiscalGraos>
                {
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.EntradaParaDeposito.Value,
                        NotaFiscalGraosCertificados =
                        {
                            new NotaFiscalGraosCertificado()
                            {
                                CertificacaoNumero = "1234",
                                CertificadoEmissorCodigo = 1,
                                CertificadoEmissorNome = "Emissor 1",
                                CertificadoEmissorId = Guid.NewGuid()
                            },
                            new NotaFiscalGraosCertificado()
                            {
                                CertificacaoNumero = "5678",
                                CertificadoEmissorCodigo = 2,
                                CertificadoEmissorNome = "Emissor 2",
                                CertificadoEmissorId = Guid.NewGuid()
                            }
                        }
                    },
                    new NotaFiscalGraos()
                    {
                        NotaFiscalGraosTipo = NotaFiscalGraosTipo.EntradaParaDeposito.Value,
                        NotaFiscalGraosCertificados =
                        {
                            new NotaFiscalGraosCertificado()
                            {
                                CertificacaoNumero = "9012",
                                CertificadoEmissorCodigo = 3,
                                CertificadoEmissorNome = "Emissor 3",
                                CertificadoEmissorId = Guid.NewGuid()
                            }
                        }
                    }
                });

            _mocker.GetMock<IFazendaCertificacaoMovimentacaoRepository>().Setup(x => x.GetCertificacoesEmVigencia(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime>()));

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            _mocker.GetMock<IRegistroDePortariaRepository>().Setup(x => x.Add(It.IsAny<RegistroDePortaria>()))
                .Callback((RegistroDePortaria r) =>
                {
                    registroDePortaria = new RegistroDePortariaBuilder()
                        .ComRegistroDePortariaId(r.Id)
                        .ComFilialId(r.FilialId)
                        .ComTipoGrao(r.TipoGrao)
                        .ComTipoOperacaoPortaria(r.TipoOperacaoPortaria)
                        .ComPlaca(r.Placa)
                        .ComMotorista(r.Motorista)
                        .ComData(r.Data)
                        .ComNotasFiscais(r.NotasFiscais.ToList());
                })
                .Returns(() => registroDePortaria);


            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()));

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit()).Returns(() => new CommandResponse());

            handlerCertificacoes = _mocker.Resolve<RegistroDePortariaSendoCriadoHandlerParaCertificacoes>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(registroDePortaria.NotasFiscais.Count > 0);
            Assert.True(registroDePortaria.NotasFiscais.All(x => x.PossuiCertificados()));
            Assert.True(registroDePortaria.NotasFiscais.First().NotaFiscalGraosCertificados.Count == 2);
            Assert.True(registroDePortaria.NotasFiscais.Last().NotaFiscalGraosCertificados.Count == 1);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Once);
            _mocker.GetMock<IFazendaCertificacaoMovimentacaoRepository>().Verify(x => x.GetCertificacoesEmVigencia(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime>()), Times.Never);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);
        }

        [Fact(DisplayName = "GeraTicketPortariaDesembarqueParaEntradaDepositoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_ValidoSemCertificacoesNasNotasFiscais_Handle_DeveAdicionarRegistroDePortaria_ComCertificacoesVindasDaMovimentacaoCadastroFazendasAtreladas()
        {
            //Arrange
            RegistroDePortariaSendoCriadoHandlerParaCertificacoes handlerCertificacoes = null;

            DomainEvent.Register<RegistroDePortariaSendoCriadoEvent>(ev => {
                _eventRegistroDePortaria = ev;
                handlerCertificacoes.Handle(_eventRegistroDePortaria);
            });

            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandValido = GeraTicketDesembarqueParaDepositoValidoCommand();

            RegistroDePortaria registroDePortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IMotoristaRepository>().Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new MotoristaBuilder());

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

            _mocker.GetMock<IFazendaCertificacaoMovimentacaoRepository>().Setup(x => x.GetCertificacoesEmVigencia(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => new List<FazendaCertificacao>()
                {
                    new FazendaCertificacaoMovimentacaoBuilder()
                       .ComFazendaCertificacaoEmissor(new FazendaCertificacaoEmissorBuilder())
                });

            var handler = _mocker.Resolve<GeraTicketPortariaCommandHandler>();

            _mocker.GetMock<IRegistroDePortariaRepository>().Setup(x => x.Add(It.IsAny<RegistroDePortaria>()))
                .Callback((RegistroDePortaria r) =>
                {
                    registroDePortaria = new RegistroDePortariaBuilder()
                        .ComRegistroDePortariaId(r.Id)
                        .ComFilialId(r.FilialId)
                        .ComTipoGrao(r.TipoGrao)
                        .ComTipoOperacaoPortaria(r.TipoOperacaoPortaria)
                        .ComPlaca(r.Placa)
                        .ComMotorista(r.Motorista)
                        .ComData(r.Data)
                        .ComNotasFiscais(r.NotasFiscais.ToList());
                })
                .Returns(() => registroDePortaria);

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()));

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            handlerCertificacoes = _mocker.Resolve<RegistroDePortariaSendoCriadoHandlerParaCertificacoes>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(registroDePortaria.NotasFiscais.Count > 0);
            Assert.True(registroDePortaria.NotasFiscais.All(x => x.PossuiCertificados()));
            Assert.True(registroDePortaria.NotasFiscais.First().NotaFiscalGraosCertificados.Count == 1);
            Assert.True(registroDePortaria.NotasFiscais.Last().NotaFiscalGraosCertificados.Count == 1);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalRepository>().Verify(x => x.GetByListaDeIds(It.IsAny<IEnumerable<Guid>>()), Times.Once);
            _mocker.GetMock<IMotoristaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortaria>()), Times.Once);
            _mocker.GetMock<IFazendaCertificacaoMovimentacaoRepository>().Verify(x => x.GetCertificacoesEmVigencia(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime>()), Times.AtLeastOnce);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);
        }

        private static GeraTicketPortariaDesembarqueParaEntradaDepositoCommand GeraTicketDesembarqueParaDepositoValidoCommand()
        {
            List<GeraTicketPortariaNotaFiscalCommand> notasFiscais = new List<GeraTicketPortariaNotaFiscalCommand>()
            {
                new GeraTicketPortariaNotaFiscalCommand(Guid.NewGuid()),
                new GeraTicketPortariaNotaFiscalCommand(Guid.NewGuid())
            };

            var commandValido =
                new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComTipoGrao(TipoGrao.Cafe.Value)
                .ComPlacaNumero("OPH0036")
                .ComMotoristaId(Guid.NewGuid())
                .ComNotasFiscais(notasFiscais);

            return commandValido;
        }

        private static GeraTicketPortariaDesembarqueParaEntradaDepositoCommand GeraTicketDesembarqueParaDepositoSemNotasFiscaisCommand()
        {
            var commandValidoSemNotasFiscais =
                new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                    .ComFilialId(Guid.NewGuid())
                    .ComTipoGrao(TipoGrao.Cafe.Value)
                    .ComPlacaNumero("OPH0036")
                    .ComMotoristaId(Guid.NewGuid());

            return commandValidoSemNotasFiscais;
        }
    }
}