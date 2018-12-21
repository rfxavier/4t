using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects
{
    public class BoletimDocumentoTests
    {
        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "BoletimDocumento Value Object")]
        void BoletimDocumento_ComDocumentosNulos_Construtor_DeveInstanciar_IdsVazios()
        {
            //Arrange
            BoletimDocumento boletimDocumento = null;

            //Act
            boletimDocumento = new BoletimDocumento(null, null, null, null, null, 0);

            //Assert
            Assert.Equal(Guid.Empty, boletimDocumento.DocumentoEntradaId);
            Assert.Equal(Guid.Empty, boletimDocumento.InstrucaoServicoId);
            Assert.Equal(Guid.Empty, boletimDocumento.TransferenciaId);
            Assert.Equal(Guid.Empty, boletimDocumento.OrdemCarregamentoId);
        }

        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "BoletimDocumento Value Object")]
        void BoletimDocumento_ComDocumentosPreenchidos_Construtor_DeveInstanciar_IdsCorretamente()
        {
            //Arrange
            BoletimDocumento boletimDocumento = null;
            var documentoEntradaId = Guid.NewGuid();
            var instrucaoServicoId = Guid.NewGuid();
            var transferenciaId = Guid.NewGuid();
            var ordemCarregamentoId = Guid.NewGuid();

            //Act
            boletimDocumento = new BoletimDocumento(null,
                new DocumentoEntradaBuilder()
                    .ComDocumentoEntradaId(documentoEntradaId),
                new InstrucaoServicoBuilder()
                    .ComInstrucaoServicoId(instrucaoServicoId),
                new TransferenciaBuilder()
                    .ComTransferenciaId(transferenciaId),
                new OrdemCarregamentoBuilder()
                    .ComOrdemCarregamentoId(ordemCarregamentoId), 0);

            //Assert
            Assert.Equal(documentoEntradaId, boletimDocumento.DocumentoEntradaId);
            Assert.Equal(instrucaoServicoId, boletimDocumento.InstrucaoServicoId);
            Assert.Equal(transferenciaId, boletimDocumento.TransferenciaId);
            Assert.Equal(ordemCarregamentoId, boletimDocumento.OrdemCarregamentoId);
        }
    }
}