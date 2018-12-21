using s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Commands.Builders;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Commands.Scopes
{
    public class RegistraBoletimCommandScopesTests
    {
        public RegistraBoletimCommandScopesTests()
        {
            DomainEvent.ClearCallbacks();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "PossuiFilialInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_SemFilialInformada_PossuiFilialInformada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder();

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiFilialInformada(null);

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Filial não informada");
        }

        [Fact(DisplayName = "PossuiFilialInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComFilialInformada_PossuiFilialInformada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder();

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiFilialInformada(new FilialBuilder());

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiUsuarioInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_SemUsuarioInformado_PossuiUsuarioInformado_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder();

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiUsuarioInformado(null);

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Usuário não informado");
        }

        [Fact(DisplayName = "PossuiUsuarioInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComUsuarioInformado_PossuiUsuarioInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder();

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiUsuarioInformado(new UsuarioBuilder());

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiNumeroInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_SemNumeroInformado_PossuiNumeroInformado_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComNumero(String.Empty);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiNumeroInformado();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Número do boletim não informado");
        }

        [Fact(DisplayName = "PossuiNumeroInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComNumeroInformado_PossuiNumeroInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComNumero("12345A");

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiNumeroInformado();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiDataInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_SemDataInformada_PossuiDataInformada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComData(DateTime.MinValue);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiDataInformada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Data do boletim não informada");
        }

        [Fact(DisplayName = "PossuiDataInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComDataInformado_PossuiDataInformada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComData(new DateTime(2018, 2, 24));

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiDataInformada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiItemInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_SemItemInformado_PossuiItemInformado_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComItem(string.Empty);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiItemInformado();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Número do item do boletim não informado");
        }

        [Fact(DisplayName = "PossuiItemInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComItemInformado_PossuiItemInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComItem("12345A");

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiItemInformado();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }


        [Fact(DisplayName = "PossuiBoletimDocumentoInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_SemBoletimDocumentoInformado_PossuiBoletimDocumentoInformado_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComBoletimDocumentoNumero(0);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiBoletimDocumentoInformado();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento do boletim não informado");
        }

        [Fact(DisplayName = "PossuiBoletimDocumentoInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComBoletimDocumentoInformado_PossuiBoletimDocumentoInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComBoletimDocumentoNumero(1);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiBoletimDocumentoInformado();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiLoteInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_SemLoteInformado_PossuiLoteInformado_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComLoteNumero(string.Empty);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLoteInformado();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Número do Lote não informado");
        }

        [Fact(DisplayName = "PossuiLoteInformado")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComLoteInformado_PossuiLoteInformado_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComLoteNumero("54321/A");

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLoteInformado();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiSacasMaiorQueZero")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComNumeroSacasIgualAZero_PossuiSacasMaiorQueZero_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComSacas(0);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiSacasMaiorQueZero();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Quantidade de sacas deve ser maior que zero");
        }

        [Fact(DisplayName = "PossuiSacasMaiorQueZero")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComNumeroSacasMaiorQueZero_PossuiSacasMaiorQueZero_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComSacas(15);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiSacasMaiorQueZero();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiLocalizacaoOrigemInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComLocalizacaoOrigemNaoInformada_PossuiLocalizacaoOrigemInformada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComOrigemFilialCodigo(0)
                    .ComOrigemBloco(string.Empty);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLocalizacaoOrigemInformada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Localização de origem não informada");
        }

        [Fact(DisplayName = "PossuiLocalizacaoOrigemInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComLocalizacaoOrigemInformada_PossuiLocalizacaoOrigemInformada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComOrigemFilialCodigo(1)
                    .ComOrigemBloco("100");

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLocalizacaoOrigemInformada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiLocalizacaoDestinoInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComLocalizacaoDestinoNaoInformada_PossuiLocalizacaoDestinoInformada_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComDestinoFilialCodigo(0)
                    .ComDestinoBloco(string.Empty);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLocalizacaoDestinoInformada();

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Localização de destino não informada");
        }

        [Fact(DisplayName = "PossuiLocalizacaoDestinoInformada")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComLocalizacaoDestinoInformada_PossuiLocalizacaoDestinoInformada_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand =
                (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                    .ComDestinoFilialCodigo(2)
                    .ComDestinoBloco("252");

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLocalizacaoDestinoInformada();

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "PossuiLocalizacoesOrigemDestinoDistintas")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComLocalizacoesOrigemDestinoIguais_PossuiLocalizacoesOrigemDestinoDistintas_DeveRetornarTodosOsErros()
        {
            //Arange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder();

            var filialId = Guid.NewGuid();
            var armazem = new ArmazemBuilder()
                .ComArmazemId(Guid.NewGuid());
            var quadra = "36";
            var bloco = "108";
            var local = new LocalBuilder()
                .ComLocalId(Guid.NewGuid());
            var pilha = new PilhaBuilder()
                .ComPilhaId(Guid.NewGuid());

            var localizacaoOrigem = new Localizacao(filialId, armazem, quadra, bloco, local, pilha);
            var localizacaoDestino = new Localizacao(filialId, armazem, quadra, bloco, local, pilha);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLocalizacoesOrigemDestinoDistintas(localizacaoOrigem, localizacaoDestino);

            //Assert
            Assert.False(resultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Localizações de origem e destino devem ser diferentes");
        }

        [Fact(DisplayName = "PossuiLocalizacoesOrigemDestinoDistintas")]
        [Trait("Category", "RegistraBoletimCommand Scopes")]
        public void
            RegistraBoletimCommandScope_ComLocalizacoesOrigemDestinoDistintas_PossuiLocalizacoesOrigemDestinoDistintas_DeveRetornarSucesso()
        {
            //Arange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder();

            var filialId1 = Guid.NewGuid();
            var armazemId1 = new ArmazemBuilder()
                .ComArmazemId(Guid.NewGuid());
            var quadra1 = "57";
            var bloco1 = "118";
            var localId1 = new LocalBuilder()
                .ComLocalId(Guid.NewGuid());
            var pilhaId1 = new PilhaBuilder()
                .ComPilhaId(Guid.NewGuid());

            var filialId2 = Guid.NewGuid();
            var armazemId2 = new ArmazemBuilder()
                .ComArmazemId(Guid.NewGuid());
            var quadra2 = "36";
            var bloco2 = "108";
            var localId2 = new LocalBuilder()
                .ComLocalId(Guid.NewGuid());
            var pilhaId2 = new PilhaBuilder()
                .ComPilhaId(Guid.NewGuid());

            var localizacaoOrigem = new Localizacao(filialId1, armazemId1, quadra1, bloco1, localId1, pilhaId1);
            var localizacaoDestino = new Localizacao(filialId2, armazemId2, quadra2, bloco2, localId2, pilhaId2);

            //Act
            var resultado = registraBoletimDocumentoEntradaCommand.PossuiLocalizacoesOrigemDestinoDistintas(localizacaoOrigem, localizacaoDestino);

            //Assert
            Assert.True(resultado);
            Assert.Equal(0, _notifications.Count);
        }
    }
}