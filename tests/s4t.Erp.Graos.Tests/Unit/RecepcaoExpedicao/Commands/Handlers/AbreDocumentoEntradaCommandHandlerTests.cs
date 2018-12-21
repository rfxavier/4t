using AutoMoq;
using Moq;
using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Handlers;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Results;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Commands.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Commands.Handlers
{
    public class AbreDocumentoEntradaCommandHandlerTests
    {
        private readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        public AbreDocumentoEntradaCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "AbreDocumentoEntradaCommand Handle")]
        [Trait("Category", "AbreDocumentoEntradaCommandHandler")]
        void AbreDocumentoEntradaCommand_ComPropriedadesQueNaoBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            AbreDocumentoEntradaCommand commandInvalido = new AbreDocumentoEntradaCommandBuilder()
                .ComTipoOperacao(0);

            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Tipo da operação não é válido");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Never);
            _mocker.GetMock<INotaFiscalGraosRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterProximaNumeracao(It.IsAny<Guid>()), Times.Never);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Add(It.IsAny<DocumentoEntrada>()), Times.Never);
        }

        [Fact(DisplayName = "AbreDocumentoEntradaCommand Handle")]
        [Trait("Category", "AbreDocumentoEntradaCommandHandler")]
        void AbreDocumentoEntradaCommand_ComPropriedadesQueBuscamDeRepositorioInvalidas_Handle_DeveRetornarTodosErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            AbreDocumentoEntradaCommand commandInvalido = new AbreDocumentoEntradaCommandBuilder()
                .ComTipoOperacao(1)
                .ComFilialId(Guid.Empty)
                .ComNotaFiscalGraosId(Guid.Empty);

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => null);

            _mocker.GetMock<INotaFiscalGraosRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandInvalido);

            //Assert
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial não informada");
            Assert.Contains(_notifications, e => e.Value == "Nota Fiscal Grãos não informada");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalGraosRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterProximaNumeracao(It.IsAny<Guid>()), Times.Never);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Add(It.IsAny<DocumentoEntrada>()), Times.Never);
        }

        [Fact(DisplayName = "AbreDocumentoEntradaCommand Handle")]
        [Trait("Category", "AbreDocumentoEntradaCommandHandler")]
        void AbreDocumentoEntradaCommand_Valido_ComNotaFiscalGraosJaVinculada_Handle_DeveRetornarErro()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            AbreDocumentoEntradaCommand commandValido = new AbreDocumentoEntradaCommandBuilder()
                .ComTipoOperacao(1)
                .ComFilialId(Guid.NewGuid())
                .ComNotaFiscalGraosId(Guid.NewGuid());

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<INotaFiscalGraosRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new NotaFiscalGraos()
                {
                    DocumentoEntrada = new DocumentoEntradaBuilder()
                });

            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            handler.Handle(commandValido);

            //Assert
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Nota Fiscal Grãos já vinculada a Documento Entrada");

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalGraosRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterProximaNumeracao(It.IsAny<Guid>()), Times.Never);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Add(It.IsAny<DocumentoEntrada>()), Times.Never);
        }

        [Fact(DisplayName = "AbreDocumentoEntradaCommand Handle")]
        [Trait("Category", "AbreDocumentoEntradaCommandHandler")]
        void AbreDocumentoEntradaCommand_Valido_Handle_DeveRetornarSucesso_SeAbriuDocumentoEntrada()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            DocumentoEntrada documentoEntrada = null;

            AbreDocumentoEntradaCommand commandValido = new AbreDocumentoEntradaCommandBuilder()
                .ComTipoOperacao(1)
                .ComFilialId(Guid.NewGuid())
                .ComNotaFiscalGraosId(Guid.NewGuid());

            _mocker.GetMock<IFilialRepository>()
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .Returns(() => new FilialBuilder());

            _mocker.GetMock<INotaFiscalGraosRepository>()
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(() => new NotaFiscalGraos()
                {
                    DocumentoEntrada = null
                });

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.ObterProximaNumeracao(It.IsAny<Guid>()))
                .Returns(1);

            _mocker.GetMock<IDocumentoEntradaRepository>()
                .Setup(x => x.Add(It.IsAny<DocumentoEntrada>()))
                .Callback((DocumentoEntrada d) =>
                {
                    documentoEntrada = new DocumentoEntradaBuilder()
                    .ComDocumentoEntradaId(d.Id)
                    .ComFilialId(d.FilialId)
                    .ComNumero(d.Numero)
                    .ComData(d.Data)
                    .ComTipoOperacao(d.TipoOperacao)
                    .ComNotaFiscalGraos(d.NotaFiscalGraos);
                })
                .Returns(() => documentoEntrada);

            var handler = _mocker.Resolve<DocumentoEntradaCommandHandler>();

            //Act
            var commandResult = handler.Handle(commandValido) as AbreDocumentoEntradaCommandResult;

            //Assert
            Assert.Equal(0, _notifications.Count);
            Assert.True(documentoEntrada.NotaFiscalGraos != null);
            Assert.True(documentoEntrada.Numero != 0);
            Assert.True(documentoEntrada.DocumentoEntradaStatus == DocumentoEntradaStatus.Aberto);

            Assert.True(commandResult != null && commandResult.DocumentoEntradaId != Guid.Empty);

            _mocker.GetMock<IFilialRepository>().Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<INotaFiscalGraosRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.ObterProximaNumeracao(It.IsAny<Guid>()), Times.Once());
            _mocker.GetMock<IDocumentoEntradaRepository>().Verify(x => x.Add(It.IsAny<DocumentoEntrada>()), Times.Once());
        }


    }
}