using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Nucleo.Entities;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs
{
    public static class AbreDocumentoEntradaCommandScopes
    {
        public static bool PossuiFilialInformada(this AbreDocumentoEntradaCommand abreDocumentoEntradaCommand,
            Filial filial)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(filial, "Filial não informada")
            );
        }

        public static bool PossuiNotaFiscalGraosInformada(this AbreDocumentoEntradaCommand abreDocumentoEntradaCommand, NotaFiscalGraos notaFiscalGraos)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(notaFiscalGraos, "Nota Fiscal Grãos não informada")
            );
        }

        public static bool PossuiNotaFiscalGraosDesvinculada(this AbreDocumentoEntradaCommand abreDocumentoEntradaCommand, NotaFiscalGraos notaFiscalGraos)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(notaFiscalGraos != null && notaFiscalGraos.DocumentoEntrada == null,
                    "Nota Fiscal Grãos já vinculada a Documento Entrada")
            );
        }


        public static bool PossuiTipoOperacaoValido(this AbreDocumentoEntradaCommand abreDocumentoEntradaCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(
                    abreDocumentoEntradaCommand.TipoOperacao >= 1 && abreDocumentoEntradaCommand.TipoOperacao <= 1,
                    "Tipo da operação não é válido")
            );
        }
    }
}