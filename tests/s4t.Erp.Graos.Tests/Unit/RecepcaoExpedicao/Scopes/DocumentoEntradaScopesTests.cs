using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Scopes
{
    public class DocumentoEntradaScopesTests
    {
        public DocumentoEntradaScopesTests()
        {
            DomainEvent.ClearCallbacks();

        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "PossuiStatusIgualEmAberto")]
        [Trait("Category", "DocumentoEntrada Scopes")]
        void DocumentoEntrada_ComStatusDiferenteDeAberto_PossuiStatusIgualEmAberto_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var documentoEntrada = (DocumentoEntrada)new DocumentoEntradaBuilder();

            //Act
            documentoEntrada.TrocaStatusParaComplementadoComLotesEPesos();

            //Assert
            Assert.False(documentoEntrada.PossuiStatusIgualEmAberto());
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento de Entrada não está no status Em Aberto");
        }

        [Fact(DisplayName = "PossuiStatusIgualEmAberto")]
        [Trait("Category", "DocumentoEntrada Scopes")]
        void DocumentoEntrada_ComStatusIgualAberto_PossuiStatusIgualEmAberto_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var documentoEntrada = (DocumentoEntrada)new DocumentoEntradaBuilder();

            //Act

            //Assert
            Assert.True(documentoEntrada.PossuiStatusIgualEmAberto());
            Assert.Equal(0, _notifications.Count);
        }

    }

}