using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Enums;
using System;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs
{
    public static class RegistraBoletimDocumentoEntradaCommandScopes
    {
        public static Lote ObterLote(this RegistraBoletimDocumentoEntradaCommand registraBoletimDocumentoEntradaCommand, BoletimDocumento boletimDocumento)
        {
            Lote lote = null;

            if (!boletimDocumento.EstaValido()) return null;

            var documentoEntrada = boletimDocumento.DocumentoEntrada;

            if (documentoEntrada.DocumentoEntradaStatus == DocumentoEntradaStatus.Aberto)
            {
                //if (registraBoletimDocumentoEntradaCommand.PossuiLoteJaExistente(loteRepository)) return null;

                lote = new Lote(Guid.NewGuid(), registraBoletimDocumentoEntradaCommand.FilialId, registraBoletimDocumentoEntradaCommand.LoteNumero,
                    registraBoletimDocumentoEntradaCommand.Sacas, 0, TipoGrao.FromValue(registraBoletimDocumentoEntradaCommand.TipoGrao), null, null,
                    documentoEntrada.NotaFiscalGraos.DestinatarioCadastroId,
                    documentoEntrada.NotaFiscalGraos.DestinatarioFazendaId, documentoEntrada);
            }
            else
            {
                lote = documentoEntrada.Lotes.ToList().FirstOrDefault(l => l.FilialId == registraBoletimDocumentoEntradaCommand.FilialId
                                                                           && l.Numero == registraBoletimDocumentoEntradaCommand.LoteNumero);

                AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(lote, "Lote não pertence a Documento de Entrada informado"));

                //if (registraBoletimDocumentoEntradaCommand.PossuiBoletimJaExistente(boletimRepository)) return null;
            }

            return lote;
        }
    }
}