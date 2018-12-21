using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Enums;
using System;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities
{
    public class DocumentoEntradaTests
    {
        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "DocumentoEntrada Entity")]
        void DocumentoEntrada_Construtor_DeveInstanciarDocumentoEntradaStatusIgualAberto()
        {
            //Arrange
            DocumentoEntrada documentoEntrada = null;

            //Act
            documentoEntrada = new DocumentoEntrada(Guid.Empty, Guid.Empty, 0, DateTime.MinValue,
                TipoOperacao.DepositoComercializacao, null);

            //Assert
            Assert.True(documentoEntrada.DocumentoEntradaStatus == DocumentoEntradaStatus.Aberto);
            Assert.True(documentoEntrada.Lotes.Count == 0);
            Assert.True(documentoEntrada.Embalagens.Count == 0);
            //Assert.True(documentoEntrada.TicketPesagem == null);
        }
    }
}