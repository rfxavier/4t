using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Enums;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities
{
    public static class DocumentoEntradaScopes
    {
        public static bool PossuiStatusIgualEmAberto(this DocumentoEntrada documentoEntrada)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(documentoEntrada.DocumentoEntradaStatus == DocumentoEntradaStatus.Aberto,
                    "Documento de Entrada não está no status Em Aberto")
            );
        }
    }
}