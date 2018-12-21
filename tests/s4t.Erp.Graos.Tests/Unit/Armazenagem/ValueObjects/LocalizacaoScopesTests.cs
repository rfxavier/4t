using Moq;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects
{
    public class LocalizacaoScopesTests
    {
        public LocalizacaoScopesTests()
        {
            DomainEvent.ClearCallbacks();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "EstaValida")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_InvalidaComArmazem_EstaValida_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoInvalida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.Empty)
                .ComArmazem(new ArmazemBuilder()
                    .ComArmazemId(Guid.NewGuid()))
                .ComPilha(null);

            //Act

            //Assert
            Assert.False(localizacaoInvalida.EstaValida());
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial da pilha não é válida");
        }

        [Fact(DisplayName = "EstaValida")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_ValidaComArmazem_EstaValida_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoValida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComArmazem(new ArmazemBuilder()
                    .ComArmazemId(Guid.NewGuid()));

            //Act

            //Assert
            Assert.True(localizacaoValida.EstaValida());
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "EstaValida")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_InvalidaSemArmazem_EstaValida_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoInvalida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.Empty)
                .ComArmazem(null)
                .ComLocal(null);

            //Act

            //Assert
            Assert.False(localizacaoInvalida.EstaValida());
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial da localização não é válida");
            Assert.Contains(_notifications, e => e.Value == "Local temporário não é válido");
        }

        [Fact(DisplayName = "EstaValida")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_ValidaSemArmazem_EstaValida_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoValida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComArmazem(null)
                .ComLocal(new LocalBuilder()
                    .ComLocalId(Guid.NewGuid()));

            //Act

            //Assert
            Assert.True(localizacaoValida.EstaValida());
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "EstaCadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_InvalidaComArmazem_EstaCadastrada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoInvalida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.Empty)
                .ComArmazem(new ArmazemBuilder()
                    .ComArmazemId(Guid.NewGuid()))
                .ComPilha(null);

            //Act
            var resultado = localizacaoInvalida.EstaCadastrada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Pilha não cadastrada");
        }

        [Fact(DisplayName = "EstaCadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_ValidaComArmazem_EstaCadastrada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoValida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComArmazem(new ArmazemBuilder()
                    .ComArmazemId(Guid.NewGuid()))
                .ComPilha(new PilhaBuilder()
                    .ComPilhaId(Guid.NewGuid()));

            //Act
            var resultado = localizacaoValida.EstaCadastrada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "EstaCadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_InvalidaSemArmazem_EstaCadastrada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoInvalida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.Empty)
                .ComArmazem(null)
                .ComLocal(null);

            //Act
            var resultado = localizacaoInvalida.EstaCadastrada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Local não cadastrado");
        }

        [Fact(DisplayName = "EstaCadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_ValidaSemArmazem_EstaCadastrada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoValida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComArmazem(null)
                .ComLocal(new LocalBuilder()
                    .ComLocalId(Guid.NewGuid()));

            //Act
            var resultado = localizacaoValida.EstaCadastrada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "EstaValidaECadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_InvalidaComArmazem_EstaValidaECadastrada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoInvalida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.Empty)
                .ComArmazem(new ArmazemBuilder()
                    .ComArmazemId(Guid.NewGuid()))
                .ComLocal(null);

            //Act
            var resultado = localizacaoInvalida.EstaValidaECadastrada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial da pilha não é válida");
        }

        [Fact(DisplayName = "EstaValidaECadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_ValidaComArmazem_EstaValidaECadastrada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoValida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComArmazem(new ArmazemBuilder()
                    .ComArmazemId(Guid.NewGuid()))
                .ComPilha(new PilhaBuilder()
                    .ComPilhaId(Guid.NewGuid()));

            //Act
            var resultado = localizacaoValida.EstaValidaECadastrada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }


        [Fact(DisplayName = "EstaValidaECadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_InvalidaSemArmazem_EstaValidaECadastrada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoInvalida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.Empty)
                .ComArmazem(null)
                .ComLocal(null);

            //Act
            var resultado = localizacaoInvalida.EstaValidaECadastrada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(2, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial da localização não é válida");
            Assert.Contains(_notifications, e => e.Value == "Local temporário não é válido");
        }

        [Fact(DisplayName = "EstaValidaECadastrada")]
        [Trait("Category", "Localizacao Value Object Scopes")]
        void Localizacao_ValidaSemArmazem_EstaValidaECadastrada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var localizacaoValida = (Localizacao) new LocalizacaoBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComArmazem(null)
                .ComLocal(new LocalBuilder()
                    .ComLocalId(Guid.NewGuid()));

            //Act
            var resultado = localizacaoValida.EstaValidaECadastrada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }
    }
}