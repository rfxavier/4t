using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Commands.Builders;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects.Builders;
using s4t.Erp.Graos.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Commands.Scopes
{
    public class RegistraBoletimDocumentoEntradaCommandScopesTests
    {
        public RegistraBoletimDocumentoEntradaCommandScopesTests()
        {
            DomainEvent.ClearCallbacks();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "ObterLote")]
        [Trait("Category", "RegistraBoletimDocumentoEntradaCommand Scopes")]
        public void
            RegistraBoletimDocumentoEntradaCommandScope_ComBoletimDocumentoInvalido_ObterLote_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder();

            var boletimDocumento = new BoletimDocumentoBuilder()
                .ComSerie(BoletimSerie.Nde)
                .ComDocumentoEntrada(null);

            //Act
            var loteResultado = registraBoletimDocumentoEntradaCommand.ObterLote(boletimDocumento);

            //Assert
            Assert.Null(loteResultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Documento do boletim está inválido");
        }

        [Fact(DisplayName = "ObterLote")]
        [Trait("Category", "RegistraBoletimDocumentoEntradaCommand Scopes")]
        public void
            RegistraBoletimDocumentoEntradaCommandScope_ComDocumentoEntradaStatusAberto_ObterLote_DeveRetornarNovoLote()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComNumero("7890/A")
                .ComSacas(50)
                .ComTipoGrao(TipoGrao.Milho.Value);

            var notaFiscalGraos = new NotaFiscalGraos()
            {
                DestinatarioCadastroId = Guid.NewGuid(),
                DestinatarioFazendaId = Guid.NewGuid()
            };

            var documentoEntrada = (DocumentoEntrada) new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid())
                .ComNotaFiscalGraos(notaFiscalGraos);

            var boletimDocumento = new BoletimDocumentoBuilder()
                .ComSerie(BoletimSerie.Nde)
                .ComDocumentoEntrada(documentoEntrada);

            //Act
            var loteResultado = registraBoletimDocumentoEntradaCommand.ObterLote(boletimDocumento);

            //Assert
            Assert.NotNull(loteResultado);
            Assert.NotEqual(Guid.Empty, loteResultado.Id);

            Assert.Equal(registraBoletimDocumentoEntradaCommand.FilialId, loteResultado.FilialId);
            Assert.Equal(registraBoletimDocumentoEntradaCommand.LoteNumero, loteResultado.Numero);
            Assert.Equal(registraBoletimDocumentoEntradaCommand.Sacas, loteResultado.Sacas);
            Assert.Equal(0, loteResultado.Peso);
            Assert.Equal(TipoGrao.FromValue(registraBoletimDocumentoEntradaCommand.TipoGrao), loteResultado.TipoGrao);
            Assert.Null(loteResultado.Embalagem);
            Assert.Null(loteResultado.TicketPesagemMovimentacao);
            Assert.Equal(notaFiscalGraos.DestinatarioCadastroId, loteResultado.CadastroTitularId);
            Assert.Equal(notaFiscalGraos.DestinatarioFazendaId, loteResultado.FazendaTitularId);

            Assert.Equal(0, _notifications.Count);
        }

        [Fact(DisplayName = "ObterLote")]
        [Trait("Category", "RegistraBoletimDocumentoEntradaCommand Scopes")]
        public void
            RegistraBoletimDocumentoEntradaCommandScope_ComDocumentoEntradaStatusDiferenteDeAbertoComLoteNaoEncontrado_ObterLote_DeveRetornarTodosOsErros()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand) new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComLoteNumero("1234/A");

            var lote1 = new LoteBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComNumero("4321/A");

            var lote2 = new LoteBuilder()
                .ComFilialId(Guid.NewGuid())
                .ComNumero("4321/B");

            var documentoEntrada = (DocumentoEntrada) new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid())
                .ComLotes(new List<Lote>() {lote1, lote2});

            documentoEntrada.TrocaStatusParaComplementadoComLotesEPesos();

            var boletimDocumento = new BoletimDocumentoBuilder()
                .ComSerie(BoletimSerie.Nde)
                .ComDocumentoEntrada(documentoEntrada);

            //Act
            var loteResultado = registraBoletimDocumentoEntradaCommand.ObterLote(boletimDocumento);

            //Assert'
            Assert.Null(loteResultado);
            Assert.Equal(1, _notifications.Count);
            Assert.Contains(_notifications, e => e.Value == "Lote não pertence a Documento de Entrada informado");
        }

        [Fact(DisplayName = "ObterLote")]
        [Trait("Category", "RegistraBoletimDocumentoEntradaCommand Scopes")]
        public void
            RegistraBoletimDocumentoEntradaCommandScope_ComDocumentoEntradaStatusDiferenteDeAbertoComLoteEncontrado_ObterLote_DeveRetornarSucesso()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var filialId = Guid.NewGuid();

            var registraBoletimDocumentoEntradaCommand = (RegistraBoletimDocumentoEntradaCommand)new RegistraBoletimDocumentoEntradaCommandBuilder()
                .ComFilialId(filialId)
                .ComLoteNumero("5678/B");

            var lote1 = new LoteBuilder()
                .ComFilialId(filialId)
                .ComNumero("5678/A");

            var lote2 = new LoteBuilder()
                .ComFilialId(filialId)
                .ComNumero("5678/B");

            var lote3 = new LoteBuilder()
                .ComFilialId(filialId)
                .ComNumero("5678/C");

            var documentoEntrada = (DocumentoEntrada)new DocumentoEntradaBuilder()
                .ComDocumentoEntradaId(Guid.NewGuid())
                .ComLotes(new List<Lote>() { lote1, lote2, lote3 });

            documentoEntrada.TrocaStatusParaComplementadoComLotesEPesos();

            var boletimDocumento = new BoletimDocumentoBuilder()
                .ComSerie(BoletimSerie.Nde)
                .ComDocumentoEntrada(documentoEntrada);

            //Act
            var loteResultado = registraBoletimDocumentoEntradaCommand.ObterLote(boletimDocumento);

            //Assert
            Assert.NotNull(loteResultado);

            Assert.Equal(registraBoletimDocumentoEntradaCommand.FilialId, loteResultado.FilialId);
            Assert.Equal(registraBoletimDocumentoEntradaCommand.LoteNumero, loteResultado.Numero);

            Assert.Equal(0, _notifications.Count);
        }
    }
}