using s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using System;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Commands.Inputs
{
    public class CommandsInputsTests
    {
        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "RegistraBoletimCommand Inputs")]
        public void RegistraBoletimDocumentoEntradaCommand_Construtor_DeveInstanciarBoletimDocumentoSerie_IgualANde()
        {
            //Arrange
            RegistraBoletimDocumentoEntradaCommand registraBoletimDocumentoEntradaCommand = null;

            //Act
            registraBoletimDocumentoEntradaCommand = new RegistraBoletimDocumentoEntradaCommand(Guid.Empty, Guid.Empty, string.Empty, DateTime.MinValue,
                string.Empty, 0, 0, string.Empty, 0, 0, String.Empty, String.Empty, String.Empty, 0, String.Empty, String.Empty, String.Empty, String.Empty, 0);

            //Assert
            Assert.True(registraBoletimDocumentoEntradaCommand.BoletimDocumentoSerie == BoletimSerie.Nde.Value);
        }
    }
}