using System;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs
{
    public static class ComplementaDocumentoEntradaComLotesEPesosCommandScopes
    {
        public static bool PossuiDocumentoEntradaInformado(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command,
            DocumentoEntrada documentoEntrada)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(documentoEntrada != null && documentoEntrada.Id != Guid.Empty, "Documento de Entrada não informado")
            );
        }

        public static bool PossuiListaDeLotesInformada(this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.Lotes != null && command.Lotes.Any(), "Lista de Lotes não informada")
            );
        }

        public static bool PossuiTodosLotesComNumeroInformado(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.Lotes.All(l => !string.IsNullOrEmpty(l.Numero)),
                    "Existem Lotes com número não informado")
            );
        }

        public static bool PossuiTodosLotesComSacasMaiorQueZero(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.Lotes.All(l => l.Sacas > 0), "Existem Lotes com sacas inválidas")
            );
        }

        public static bool PossuiTodosLotesComPesoMaiorQueZero(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.Lotes.All(l => l.Peso > 0), "Existem Lotes com peso inválido")
            );
        }

        public static bool PossuiAlgumLoteComEmbalagemInformada(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return command.Lotes.Any(l => l.EmbalagemId != Guid.Empty);
        }

        public static List<Guid> ListaDeEmbalagemIds(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return command.Lotes.Where(l => l.EmbalagemId != Guid.Empty).Select(l => l.EmbalagemId).ToList();
        }

        public static string ListaDeEmbalagemIdsToString(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            var lista = command.ListaDeEmbalagemIds().Any() ? "'" + string.Join("','", command.ListaDeEmbalagemIds()) + "'" : "'" + string.Join("','", new List<Guid> {Guid.Empty}) + "'";

            return lista;
        }

        public static bool PossuiAlgumLoteComTicketPesagemMovimentacaoInformado(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return command.Lotes.Any(l => l.TicketPesagemMovimentacaoId != Guid.Empty);
        }

        public static List<Guid> ListaDeTicketPesagemMovimentacaoIds(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            return command.Lotes.Where(l => l.TicketPesagemMovimentacaoId != Guid.Empty).Select(l => l.TicketPesagemMovimentacaoId).ToList();
        }

        public static string ListaDeTicketPesagemMovimentacaoIdsToString(
            this ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            var lista = command.ListaDeTicketPesagemMovimentacaoIds().Any() ? "'" + string.Join("','", command.ListaDeTicketPesagemMovimentacaoIds()) + "'" : "'" + string.Join("','", new List<Guid> { Guid.Empty }) + "'";

            return lista;
        }
    }
}