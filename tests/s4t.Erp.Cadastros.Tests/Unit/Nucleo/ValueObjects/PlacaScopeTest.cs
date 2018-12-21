using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Core.Domain.ValueObjects.Placa;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Cadastros.Tests.Unit.Nucleo.ValueObjects
{
    public class PlacaScopeTest
    {
        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        public PlacaScopeTest()
        {
            DomainEvent.ClearCallbacks();

        }
        [Fact(DisplayName = "EstaValida")] 
        [Trait("Category","Placa ValueObject Scopes")]
        public void PlacaInvalida_EstaValida_DeveRetornarTodosOsErros()
        {
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var placa = new Placa("OPH06");

            Assert.False(placa.EstaValida());

            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Placa de veículo deve ser válida");
        }

        [Fact(DisplayName = "EstaValida")]
        [Trait("Category", "Placa ValueObject Scopes")]
        public void PlacaValidaOuEmBranco_EstaValida_DeveRetornarSucesso()
        {
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var placa = new Placa("OPH0036");

            Assert.True(placa.EstaValida());
            Assert.Equal(0, _notifications.Count);


            var placaEmBranco = new Placa("");

            Assert.True(placaEmBranco.EstaValida());
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "EstaValidaEPreenchida")]
        [Trait("Category", "Placa ValueObject Scopes")]
        public void PlacaEmBrancoOuInvalida_EstaValidaEPreenchida_DeveRetornarTodosOsErros()
        {
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var placaEmBranco = new Placa("");

            Assert.False(placaEmBranco.EstaValidaEPreenchida());

            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Placa de veículo é obrigatória");

            _notifications.Clear();

            var placa = new Placa("OPH06");

            Assert.False(placa.EstaValidaEPreenchida());

            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Placa de veículo deve ser válida");
        }

        [Fact(DisplayName = "EstaValidaEPreenchida")]
        [Trait("Category", "Placa ValueObject Scopes")]
        public void PlacaPreenchidaEValida_EstaValidaEPreenchida_DeveRetornarSucesso()
        {
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var placa = new Placa("OPH0036");

            Assert.True(placa.EstaValidaEPreenchida());
            Assert.Equal(0, _notifications.Count);
        }
    }
}
