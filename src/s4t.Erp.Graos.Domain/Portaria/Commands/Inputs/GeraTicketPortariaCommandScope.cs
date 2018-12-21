using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Portaria.Enums;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Inputs
{
    public static class GeraTicketPortariaCommandScope
    {
        public static bool PossuiFilialInformada(this GeraTicketPortariaCommand geraTicketPortariaCommand, Filial filial)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(filial, "Filial não informada")
            );
        }

        public static bool PossuiTipoGraoValido(this GeraTicketPortariaCommand geraTicketPortariaCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(geraTicketPortariaCommand.TipoGrao >= 1 && geraTicketPortariaCommand.TipoGrao <= 3, "Tipo do grão não é valido")
            );
        }

        public static bool PossuiTipoOperacaoValido(this GeraTicketPortariaCommand geraTicketPortariaCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(geraTicketPortariaCommand.TipoOperacaoPortaria >= 1 && geraTicketPortariaCommand.TipoOperacaoPortaria <= 4, "Tipo de operação não é válida")
            );
        }

        public static bool PossuiMotoristaComCadastroPreenchido(this GeraTicketPortariaCommand geraTicketPortariaCommand, Motorista motorista)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(motorista, "Motorista não informado")
            );
        }

        public static bool PossuiNotasFiscaisDesembarqueParaEntradaDepositoInformadas(
            this GeraTicketPortariaCommand geraTicketPortariaCommand,
            IEnumerable<NotaFiscalGraos> notasFiscais)
        {
            var tiposNotaFiscalDesembarqueParaEntradaDeposito = new[] { NotaFiscalGraosTipo.EntradaParaDeposito.Value, NotaFiscalGraosTipo.RemessaParaDeposito.Value};

            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(notasFiscais != null && geraTicketPortariaCommand.NotasFiscais.Any() && notasFiscais.Count() == geraTicketPortariaCommand.NotasFiscais.Count,
                    "Notas Fiscais para Desembarque Para Entrada Depósito não informadas"),
                AssertionConcern.AssertTrue(notasFiscais != null && notasFiscais.All(x => tiposNotaFiscalDesembarqueParaEntradaDeposito.Contains(x.NotaFiscalGraosTipo)),
                    "Notas Fiscais para Desembarque para Entrada Depósito não são do tipo correto")
            );
        }

        public static bool PossuiNotasFiscaisDesembarqueParaEntradaTransferenciaInformadas(
            this GeraTicketPortariaCommand geraTicketPortariaCommand,
            IEnumerable<NotaFiscalGraos> notasFiscais)
        {
            var tiposNotaFiscalDesembarqueParaEntradaTransferencia = new[] { NotaFiscalGraosTipo.SaidaParaTransferencia.Value };

            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(notasFiscais != null && geraTicketPortariaCommand.NotasFiscais.Any() && notasFiscais.Count() == geraTicketPortariaCommand.NotasFiscais.Count,
                    "Notas Fiscais para Desembarque Para Entrada Transferência não informadas"),
                AssertionConcern.AssertTrue(notasFiscais != null &&
                    notasFiscais.All(x => tiposNotaFiscalDesembarqueParaEntradaTransferencia.Contains(x.NotaFiscalGraosTipo)),
                    "Notas Fiscais para Desembarque para Entrada Transferência não são do tipo correto")
            );
        }
    }
}