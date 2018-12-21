using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces
{
    public interface IDocumentoEntradaRepository
    {
        DocumentoEntrada ObterPorId(Guid id);
        DocumentoEntrada Add(DocumentoEntrada documentoEntrada);
        DocumentoEntrada Update(DocumentoEntrada documentoEntrada);
        DocumentoEntrada ObterPorNumero(Guid filialId, int numero);
        int ObterProximaNumeracao(Guid filialId);
    }
}