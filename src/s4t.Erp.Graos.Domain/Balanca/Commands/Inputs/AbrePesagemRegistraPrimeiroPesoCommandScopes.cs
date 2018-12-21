using System;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Inputs
{
    public static class AbrePesagemRegistraPrimeiroPesoCommandScopes
    {
        public static bool PossuiPesoInvalido(this AbrePesagemRegistraPrimeiroPesoCommand abrePesagemRegistraPrimeiroPesoCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertIsGreaterThan(Convert.ToDecimal(abrePesagemRegistraPrimeiroPesoCommand.Peso), 0, "Peso inválido")
            );
        }

        public static bool PossuiDocumentoEntradaInformado(this AbrePesagemRegistraPrimeiroPesoCommand abrePesagemRegistraPrimeiroPesoCommand,
            DocumentoEntrada documentoEntrada)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(documentoEntrada, "Documento de Entrada não informado")
            );
        }
    }
}