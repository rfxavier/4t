using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Scopes
{
    public class CommandScopesTests
    {
        public CommandScopesTests()
        {
            DomainEvent.ClearCallbacks();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "PossuiMotoristaComCadastroPreenchido")]
        [Trait("Category", "GeraTicketPortariaCommand Scopes")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommandScope_ComMotoristaCadastradoPreenchido_PossuiMotoristaComCadastroPreenchido_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var geraTicketCommand =
                (GeraTicketPortariaDesembarqueParaEntradaDepositoCommand)new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                    .ComMotoristaId(Guid.NewGuid());

            var motorista = new MotoristaBuilder()
                .ComMotoristaId(Guid.NewGuid());

            //Act


            //Assert
            Assert.True(geraTicketCommand.PossuiMotoristaComCadastroPreenchido(motorista));
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiMotoristaComCadastroPreenchido")]
        [Trait("Category", "GeraTicketPortariaCommand Scopes")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommandScope_SemMotoristaCadastradoPreenchido_PossuiMotoristaComCadastroPreenchido_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var geraTicketCommand =
                (GeraTicketPortariaDesembarqueParaEntradaDepositoCommand)new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                    .ComMotoristaId(Guid.Empty);

            Motorista motorista = null;

            //Act


            //Assert
            Assert.False(geraTicketCommand.PossuiMotoristaComCadastroPreenchido(motorista));
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Motorista não informado");
        }

        [Theory(DisplayName = "PossuiTipoGraoValido")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [Trait("Category", "GeraTicketPortariaCommand Scopes")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommandScope_ComTipoGraoValido_PossuiTipoGraoValido_DeveRetornarSucesso(int tipoGrao)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var geraTicketCommand =
                (GeraTicketPortariaDesembarqueParaEntradaDepositoCommand)new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                    .ComTipoGrao(tipoGrao);

            //Act

            //Assert
            Assert.True(geraTicketCommand.PossuiTipoGraoValido());
            Assert.Equal(0, _notifications.Count);
        }

        [Theory(DisplayName = "PossuiTipoGraoValido")]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(5)]
        [Trait("Category", "GeraTicketPortariaCommand Scopes")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommandScope_ComTipoGraoInvalido_PossuiTipoGraoValido_DeveRetornarTodosOsErros(int tipoGrao)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var geraTicketCommand =
                (GeraTicketPortariaDesembarqueParaEntradaDepositoCommand)new GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder()
                    .ComTipoGrao(tipoGrao);

            //Act

            //Assert
            Assert.False(geraTicketCommand.PossuiTipoGraoValido());
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Tipo do grão não é valido");
        }

        [Theory(DisplayName = "PossuiTipoGraoValido")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [Trait("Category", "GeraTicketPortariaCommand Scopes")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandScope_ComTipoGraoValido_PossuiTipoGraoValido_DeveRetornarSucesso(int tipoGrao)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var geraTicketCommand =
                (GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand)new GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder()
                    .ComTipoGrao(tipoGrao);

            //Act

            //Assert
            Assert.True(geraTicketCommand.PossuiTipoGraoValido());
            Assert.Equal(0, _notifications.Count);
        }

        [Theory(DisplayName = "PossuiTipoGraoValido")]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(5)]
        [Trait("Category", "GeraTicketPortariaCommand Scopes")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandScope_ComTipoGraoInvalido_PossuiTipoGraoValido_DeveRetornarTodosOsErros(
                int tipoGrao)
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var geraTicketCommand =
                (GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand)new GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder()
                    .ComTipoGrao(tipoGrao);

            //Act

            //Assert
            Assert.False(geraTicketCommand.PossuiTipoGraoValido());
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Tipo do grão não é valido");
        }
    }
}