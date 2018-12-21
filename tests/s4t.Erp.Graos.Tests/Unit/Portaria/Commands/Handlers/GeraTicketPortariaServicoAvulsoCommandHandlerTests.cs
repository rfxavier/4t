using AutoMoq;
using Moq;
using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Portaria.Commands.Handlers;
using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;
using s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Builders;
using s4t.Erp.Graos.Tests.Unit.Portaria.Entities.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Handlers
{
    public class GeraTicketPortariaServicoAvulsoCommandHandlerTests
    {
        private readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        public GeraTicketPortariaServicoAvulsoCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "GeraTicketPortariaServicoAvulsoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaServicoAvulsoCommand_ComPropriedadesQueNaoBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var commandInvalido = new GeraTicketPortariaServicoAvulsoCommandBuilder().ComPlacaNumero("OP123");

            var handler = _mocker.Resolve<GeraTicketPortariaServicoAvulsoCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Placa de veículo deve ser válida");
        }

        [Fact(DisplayName = "GeraTicketPortariaServicoAvulsoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaServicoAvulsoCommand_ComPropriedadesQueBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var commandInvalido = new GeraTicketPortariaServicoAvulsoCommandBuilder();

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => null);

            _mocker.GetMock<IProdutoPortariaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            var handler = _mocker.Resolve<GeraTicketPortariaServicoAvulsoCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial não informada");
            Assert.Contains(_notifications, e => e.Value == "Produto Serviço Avulso não informado");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IProdutoPortariaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "GeraTicketPortariaServicoAvulsoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void GeraTicketPortariaServicoAvulsoCommand_Valido_Handle_DeveRetornarSucesso_SeGerouTicket()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaServicoAvulsoCommand commandValido = GeraTicketServicoAvulsoValidoCommand();

            TicketPortaria ticketPortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IProdutoPortariaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new ProdutoPortariaBuilder());

            _mocker.GetMock<IRegistroDePortariaServicoAvulsoRepository>()
                .Setup(x => x.Add(It.IsAny<RegistroDePortariaServicoAvulso>()));

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Callback((TicketPortaria t) =>
                {
                    ticketPortaria = new TicketPortariaBuilder()
                        .ComTicketPortariaId(t.Id)
                        .ComNumero(t.Numero)
                        .ComRegistroDePortariaServicoAvulso(t.RegistroDePortariaServicoAvulso);
                })
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handler = _mocker.Resolve<GeraTicketPortariaServicoAvulsoCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IProdutoPortariaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaServicoAvulsoRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortariaServicoAvulso>()), Times.Once);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);

            Assert.True(ticketPortaria.Id != Guid.Empty);
        }

        [Fact(DisplayName = "GeraTicketPortariaServicoAvulsoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaServicoAvulsoCommand_Valido_Handle_DeveRetornarSucesso_SeGerouTicketETicketFoiAtreladoARegistroDePortaria()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            GeraTicketPortariaServicoAvulsoCommand commandValido = GeraTicketServicoAvulsoValidoCommand();

            TicketPortaria ticketPortaria = null;

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IProdutoPortariaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new ProdutoPortariaBuilder());

            _mocker.GetMock<IRegistroDePortariaServicoAvulsoRepository>()
                .Setup(x => x.Add(It.IsAny<RegistroDePortariaServicoAvulso>()));

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Callback((TicketPortaria t) =>
                {
                    ticketPortaria = new TicketPortariaBuilder()
                        .ComTicketPortariaId(t.Id)
                        .ComNumero(t.Numero)
                        .ComRegistroDePortariaServicoAvulso(t.RegistroDePortariaServicoAvulso);
                })
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handler = _mocker.Resolve<GeraTicketPortariaServicoAvulsoCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IProdutoPortariaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaServicoAvulsoRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortariaServicoAvulso>()), Times.Once);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);

            Assert.True(ticketPortaria.Id != Guid.Empty);
            Assert.True(ticketPortaria.RegistroDePortariaServicoAvulso != null);
        }

        [Fact(DisplayName = "GeraTicketPortariaServicoAvulsoCommandHandler Handle")]
        [Trait("Category", "GeraTicketPortariaCommandHandler")]
        public void
            GeraTicketPortariaServicoAvulsoCommand_Valido_Handle_DeveRetornarErro_SeGerouTicketETicketNaoFoiAtreladoARegistroDePortaria()
        {
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            //Arrange
            GeraTicketPortariaServicoAvulsoCommand commandValido = GeraTicketServicoAvulsoValidoCommand();

            TicketPortaria ticketPortaria = new TicketPortariaBuilder()
                .ComTicketPortariaId(Guid.NewGuid())
                .ComRegistroDePortariaServicoAvulso(null);

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<IProdutoPortariaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new ProdutoPortariaBuilder());

            _mocker.GetMock<IRegistroDePortariaServicoAvulsoRepository>()
                .Setup(x => x.Add(It.IsAny<RegistroDePortariaServicoAvulso>()));

            _mocker.GetMock<ITicketPortariaRepository>().Setup(x => x.Add(It.IsAny<TicketPortaria>()))
                .Returns(() => ticketPortaria);

            _mocker.GetMock<IUnitOfWork>().Setup(x => x.Commit())
                .Returns(() => new CommandResponse());

            var handler = _mocker.Resolve<GeraTicketPortariaServicoAvulsoCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(0, _notifications.Count);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IProdutoPortariaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IRegistroDePortariaServicoAvulsoRepository>().Verify(x => x.Add(It.IsAny<RegistroDePortariaServicoAvulso>()), Times.Once);
            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.Add(It.IsAny<TicketPortaria>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(x => x.Commit(), Times.Once);

            Assert.True(ticketPortaria.Id != Guid.Empty);
            Assert.True(ticketPortaria.RegistroDePortariaServicoAvulso == null);
        }

        private static GeraTicketPortariaServicoAvulsoCommand GeraTicketServicoAvulsoValidoCommand()
        {
            var commandValido = new GeraTicketPortariaServicoAvulsoCommandBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComPlacaNumero("OPH0036")
                .ComMotoristaId(Guid.NewGuid())
                .ComProdutoPortariaId(Guid.NewGuid());

            return commandValido;
        }
    }
}