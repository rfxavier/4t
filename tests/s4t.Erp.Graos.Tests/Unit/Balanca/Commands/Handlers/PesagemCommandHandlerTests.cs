using AutoMoq;
using Moq;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Balanca.Commands.Handlers;
using s4t.Erp.Graos.Domain.Balanca.Commands.Inputs;
using s4t.Erp.Graos.Domain.Balanca.Commands.Results;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Balanca.Interfaces;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using s4t.Erp.Graos.Tests.Unit.Balanca.Commands.Builders;
using s4t.Erp.Graos.Tests.Unit.Balanca.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Portaria.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Balanca.Commands.Handlers
{
    public class PesagemCommandHandlerTests
    {
        private readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        public PesagemCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Theory(DisplayName = "AbrePesagemRegistraPrimeiroPesoCommandHandler Handle")]
        [InlineData(-30)]
        [InlineData(-0.01)]
        [InlineData(0)]
        [Trait("Category", "PesagemCommandHandler")]
        void AbrePesagemRegistraPrimeiroPesoCommand_ComPesoInvalido_Handle_DeveRetornarErro(double peso)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            AbrePesagemRegistraPrimeiroPesoCommand commandInvalido = new AbrePesagemRegistraPrimeiroPesoCommandBuilder()
                .ComPeso(peso);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Peso inválido");
        }

        [Fact(DisplayName = "AbrePesagemRegistraPrimeiroPesoCommandHandler Handle")]
        [Trait("Category", "PesagemCommandHandler")]
        void AbrePesagemRegistraPrimeiroPesoCommand_ComPropriedadesQueBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            AbrePesagemRegistraPrimeiroPesoCommand commandInvalido = new AbrePesagemRegistraPrimeiroPesoCommandBuilder()
                .ComPeso(1000)
                .ComDocumentoEntradaId(Guid.Empty);

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => null);

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.GetProximaNumeracao(It.IsAny<Guid>()))
                .Returns(() => 0);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento de Entrada não informado");

            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<ITicketPesagemRepository>().Verify(x => x.GetProximaNumeracao(It.IsAny<Guid>()), Times.Never);

        }

        [Fact(DisplayName = "AbrePesagemRegistraPrimeiroPesoCommandHandler Handle")]
        [Trait("Category", "PesagemCommandHandler")]
        void AbrePesagemRegistraPrimeiroPesoCommand_Valido_Handle_DeveRetornarSucesso_SeRegistrouPesagem_EFoiAtreladaADocumentoEntrada()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            TicketPesagem ticketPesagem = null;

            double pesoNovo = 1000;

            AbrePesagemRegistraPrimeiroPesoCommand commandValido = new AbrePesagemRegistraPrimeiroPesoCommandBuilder()
                .ComPeso(pesoNovo)
                .ComDocumentoEntradaId(Guid.NewGuid());

            Guid documentoEntradaFilialId = Guid.NewGuid();

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid())
                .ComFilialId(documentoEntradaFilialId));

            int numeroProximaNumeracao = 1;

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.GetProximaNumeracao(It.IsAny<Guid>()))
                .Returns(() => numeroProximaNumeracao);

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.Add(It.IsAny<TicketPesagem>()))
                .Callback((TicketPesagem t) =>
                {
                    ticketPesagem = new TicketPesagemBuilder()
                    .ComTicketPesagemId(t.Id)
                    .ComFilialId(t.FilialId)
                    .ComNumero(t.Numero)
                    .ComTicketPortaria(t.TicketPortaria)
                    .ComDocumentoEntrada(t.DocumentoEntrada)
                    .ComMovimentacoes(t.TicketPesagemMovimentacoes);
                })
                .Returns(() => ticketPesagem);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            var commandResult = handler.Handle(commandValido) as PesagemCommandResult;

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(ticketPesagem.Id != Guid.Empty);
            Assert.True(ticketPesagem.DocumentoEntrada != null && ticketPesagem.DocumentoEntrada.Id != Guid.Empty);
            Assert.True(ticketPesagem.FilialId == documentoEntradaFilialId);
            Assert.True(ticketPesagem.TicketPesagemMovimentacoes.Count == 1);
            Assert.True(ticketPesagem.TicketPesagemMovimentacoes.LastOrDefault().Peso == pesoNovo);


            Assert.True(commandResult != null && commandResult.NumeroTicketPesagem == numeroProximaNumeracao.ToString());

            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<ITicketPesagemRepository>().Verify(x => x.GetProximaNumeracao(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<ITicketPesagemRepository>().Verify(x => x.Add(It.IsAny<TicketPesagem>()), Times.Once);
        }

        [Theory(DisplayName = "AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandHandler Handle")]
        [InlineData(-45)]
        [InlineData(-0.02)]
        [InlineData(0)]
        [Trait("Category", "PesagemCommandHandler")]
        void AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand_ComPesoInvalido_Handle_DeveRetornarErro(double peso)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand commandInvalido = new AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandBuilder()
                .ComPeso(peso);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Peso inválido");
        }

        [Fact(DisplayName = "AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandHandler Handle")]
        [Trait("Category", "PesagemCommandHandler")]
        void AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand_ComPropriedadesQueBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand commandInvalido = new AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandBuilder()
                .ComPeso(1000)
                .ComTicketPortariaId(Guid.Empty);

            _mocker.GetMock<ITicketPortariaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.GetProximaNumeracao(It.IsAny<Guid>()))
                .Returns(() => 0);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Ticket de portaria não informado");

            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<ITicketPesagemRepository>().Verify(x => x.GetProximaNumeracao(It.IsAny<Guid>()), Times.Never);

        }

        [Fact(DisplayName = "AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandHandler Handle")]
        [Trait("Category", "PesagemCommandHandler")]
        void AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandValido_Handle_DeveRetornarSucesso_SeRegistrouPesagem_EFoiAtrelada_ATicketPortaria()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            TicketPesagem ticketPesagem = null;

            double pesoNovo = 1000;

            AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand commandValido = new AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandBuilder()
                .ComPeso(pesoNovo)
                .ComTicketPortariaId(Guid.NewGuid());

            Guid ticketPortariaFilialId = Guid.NewGuid();

            _mocker.GetMock<ITicketPortariaRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new TicketPortariaBuilder()
                .ComTicketPortariaId(Guid.NewGuid())
                .ComFilialId(ticketPortariaFilialId));

            int numeroProximaNumeracao = 1;

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.GetProximaNumeracao(It.IsAny<Guid>()))
                .Returns(() => numeroProximaNumeracao);

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.Add(It.IsAny<TicketPesagem>()))
                .Callback((TicketPesagem t) =>
                {
                    ticketPesagem = new TicketPesagemBuilder()
                    .ComTicketPesagemId(t.Id)
                    .ComFilialId(t.FilialId)
                    .ComNumero(t.Numero)
                    .ComTicketPortaria(t.TicketPortaria)
                    .ComDocumentoEntrada(t.DocumentoEntrada)
                    .ComMovimentacoes(t.TicketPesagemMovimentacoes);
                })
                .Returns(() => ticketPesagem);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            var commandResult = handler.Handle(commandValido) as PesagemCommandResult;

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(ticketPesagem.Id != Guid.Empty);
            Assert.True(ticketPesagem.TicketPortaria != null && ticketPesagem.TicketPortaria.Id != Guid.Empty);
            Assert.True(ticketPesagem.FilialId == ticketPortariaFilialId);
            Assert.True(ticketPesagem.TicketPesagemMovimentacoes.Count == 1);
            Assert.True(ticketPesagem.TicketPesagemMovimentacoes.LastOrDefault().Peso == pesoNovo);


            Assert.True(commandResult != null && commandResult.NumeroTicketPesagem == numeroProximaNumeracao.ToString());

            _mocker.GetMock<ITicketPortariaRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<ITicketPesagemRepository>().Verify(x => x.GetProximaNumeracao(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<ITicketPesagemRepository>().Verify(x => x.Add(It.IsAny<TicketPesagem>()), Times.Once);
        }

        //todo separar handler tests 1 para cada handler não deixar junto
        [Theory(DisplayName = "ContinuaPesagemRegistraPesoCommandHandler Handle")]
        [InlineData(-185)]
        [InlineData(-0.09)]
        [InlineData(0)]
        [Trait("Category", "PesagemCommandHandler")]
        void ContinuaPesagemRegistraPesoCommand_ComPesoInvalido_Handle_DeveRetornarErro(double peso)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            ContinuaPesagemRegistraPesoCommand commandInvalido = new ContinuaPesagemRegistraPesoCommandBuilder()
                .ComPeso(peso);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Peso inválido");
        }

        [Fact(DisplayName = "ContinuaPesagemRegistraPesoCommandHandler Handle")]
        [Trait("Category", "PesagemCommandHandler")]
        void
            ContinuaPesagemRegistraPesoCommand_ComPropriedadesQueBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            ContinuaPesagemRegistraPesoCommand commandInvalido = new ContinuaPesagemRegistraPesoCommandBuilder()
                .ComPeso(1000)
                .ComTicketPesagemId(Guid.Empty);

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Ticket de pesagem não informado");

            _mocker.GetMock<ITicketPesagemRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "ContinuaPesagemRegistraPesoCommandHandler Handle")]
        [Trait("Category", "PesagemCommandHandler")]
        void ContinuaPesagemRegistraPesoCommandValido_Handle_DeveRetornarSucesso_SeRegistrouPesagem_EFoiAdicionadaMovimentacao()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            TicketPesagem ticketPesagem = null;

            double pesoNovo = 1000;

            ContinuaPesagemRegistraPesoCommand commandValido = new ContinuaPesagemRegistraPesoCommandBuilder()
                .ComPeso(pesoNovo)
                .ComTicketPesagemId(Guid.NewGuid());

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new TicketPesagemBuilder()
                .ComTicketPesagemId(Guid.NewGuid())
                .ComFilialId(Guid.NewGuid())
                .ComMovimentacoes(new List<TicketPesagemMovimentacao>()
                    {
                        new TicketPesagemMovimentacao(Guid.NewGuid(), 1750)
                    }));

            _mocker.GetMock<ITicketPesagemRepository>()
                .Setup(x => x.Update(It.IsAny<TicketPesagem>()))
                .Callback((TicketPesagem t) =>
                {
                    ticketPesagem = new TicketPesagemBuilder()
                        .ComTicketPesagemId(t.Id)
                        .ComFilialId(t.FilialId)
                        .ComNumero(t.Numero)
                        .ComTicketPortaria(t.TicketPortaria)
                        .ComDocumentoEntrada(t.DocumentoEntrada)
                        .ComMovimentacoes(t.TicketPesagemMovimentacoes);
                })
                .Returns(() => ticketPesagem);

            var handler = _mocker.Resolve<PesagemCommandHandler>();

            //Act
            var commandResult = handler.Handle(commandValido) as PesagemCommandResult;

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(ticketPesagem != null && ticketPesagem.Id != Guid.Empty);
            Assert.True(ticketPesagem.TicketPesagemMovimentacoes.Count == 2);
            Assert.True(ticketPesagem.TicketPesagemMovimentacoes.LastOrDefault().Peso == pesoNovo);
        }

    }
}