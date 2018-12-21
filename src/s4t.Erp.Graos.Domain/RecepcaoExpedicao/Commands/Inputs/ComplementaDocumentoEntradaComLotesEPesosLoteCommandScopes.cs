using System;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Entities;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs
{
    public static class ComplementaDocumentoEntradaComLotesEPesosLoteCommandScopes
    {
        public static bool EncontrouEmbalagem(this ComplementaDocumentoEntradaComLotesEPesosLoteCommand command,
            IEnumerable<Embalagem> embalagens)
        {
            return command.EmbalagemId != Guid.Empty && embalagens.Any(e => command.EmbalagemId == e.Id);
        }

        public static bool PossuiEmbalagemOpcionalInformada(this ComplementaDocumentoEntradaComLotesEPesosLoteCommand command,
            IEnumerable<Embalagem> embalagens)
        {
            
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.EmbalagemId == Guid.Empty || command.EncontrouEmbalagem(embalagens),
                    $"Embalagem do Lote {command.Numero} não encontrada")
            );
        }

        public static bool PossuiEmbalagemQuantidadeMaiorQueZero(
            this ComplementaDocumentoEntradaComLotesEPesosLoteCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.QuantidadeEmbalagem > 0,
                    $"Embalagem do Lote {command.Numero} tem quantidade inválida")
            );
        }

        public static bool EncontrouTicketPesagemMovimentacao(
            this ComplementaDocumentoEntradaComLotesEPesosLoteCommand command,
            IEnumerable<TicketPesagemMovimentacao> ticketPesagemMovimentacoes)
        {
            return command.TicketPesagemMovimentacaoId != Guid.Empty &&
                   ticketPesagemMovimentacoes.Any(t => command.TicketPesagemMovimentacaoId == t.Id);
        }

        public static bool PossuiTicketPesagemMovimentacaoOpcionalInformado(
            this ComplementaDocumentoEntradaComLotesEPesosLoteCommand command,
            IEnumerable<TicketPesagemMovimentacao> ticketPesagemMovimentacoes)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.TicketPesagemMovimentacaoId == Guid.Empty || command.EncontrouTicketPesagemMovimentacao(ticketPesagemMovimentacoes),
                    $"Ticket Pesagem Movimentação do Lote {command.Numero} não encontrado")
            );
        }
    }
}