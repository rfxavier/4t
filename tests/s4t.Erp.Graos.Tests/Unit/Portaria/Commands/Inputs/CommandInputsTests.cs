using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using s4t.Erp.Graos.Domain.Portaria.Enums;
using System;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Inputs
{
    public class CommandInputsTests
    {
        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "GeraTicketPortariaCommand Inputs")]
        public void GeraTicketPortariaDesembarqueParaEntradaDepositoCommand_Construtor_DeveInstanciarTipoOperacaoIgualADesembarqueParaEntradaDeposito()
        {
            //Arrange
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommand commandGeraTicketDesembarqueParaEntradaDeposito = null;

            //Act
            commandGeraTicketDesembarqueParaEntradaDeposito = new GeraTicketPortariaDesembarqueParaEntradaDepositoCommand(Guid.NewGuid(), 1, "", Guid.NewGuid());

            //Assert
            Assert.True(commandGeraTicketDesembarqueParaEntradaDeposito.TipoOperacaoPortaria == TipoOperacaoPortaria.DesembarqueParaEntradaDeposito.Value);
        }

        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "GeraTicketPortariaCommand Inputs")]
        public void
            GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand_Construtor_DeveInstanciarTipoOperacaoIgualADesembarqueParaEntradaTransferencia()
        {
            //Arrange
            GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand commandGeraTicketDesembarqueParaEntradaTransferencia = null;

            //Act
            commandGeraTicketDesembarqueParaEntradaTransferencia = new GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand(Guid.NewGuid(),1,"",Guid.NewGuid());

            //Assert
            Assert.True(commandGeraTicketDesembarqueParaEntradaTransferencia.TipoOperacaoPortaria == TipoOperacaoPortaria.DesembarqueParaEntradaTransferencia.Value);
        }

    }
}
