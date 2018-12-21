using System;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;

namespace s4t.Erp.Graos.Domain.Armazenagem.ValueObjects
{
    public static class BoletimDocumentoScopes
    {
        public static bool PossuiSerieValida(this BoletimDocumento boletimDocumento)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(boletimDocumento.Serie, "Série do documento é inválida")
            );
        }

        public static bool EstaValido(this BoletimDocumento boletimDocumento)
        {
            var estaValido = false;

            if (boletimDocumento.PossuiSerieValida())
            {
                if (boletimDocumento.Serie == BoletimSerie.Nde)
                {
                    estaValido = boletimDocumento.DocumentoEntrada != null &&
                                 boletimDocumento.DocumentoEntrada.Id != Guid.Empty && 
                                 boletimDocumento.DocumentoEntradaId != Guid.Empty;
                }

                if (boletimDocumento.Serie == BoletimSerie.Is)
                {
                    estaValido = boletimDocumento.InstrucaoServico != null &&
                                 boletimDocumento.InstrucaoServico.Id != Guid.Empty &&
                                 boletimDocumento.InstrucaoServicoId != Guid.Empty;
                }

                if (boletimDocumento.Serie == BoletimSerie.Tr || boletimDocumento.Serie == BoletimSerie.Tis)
                {
                    estaValido = boletimDocumento.Transferencia != null &&
                                 boletimDocumento.Transferencia.Id != Guid.Empty &&
                                 boletimDocumento.TransferenciaId != Guid.Empty;
                }

                if (boletimDocumento.Serie == BoletimSerie.Re)
                {
                    estaValido = boletimDocumento.RemocaoNumero != 0;
                }

                    if (boletimDocumento.Serie == BoletimSerie.Oc)
                {
                    estaValido = boletimDocumento.OrdemCarregamento != null &&
                                 boletimDocumento.OrdemCarregamento.Id != Guid.Empty &&
                                 boletimDocumento.OrdemCarregamentoId != Guid.Empty;
                }
            }

            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(estaValido, "Documento do boletim está inválido")
            );
        }
    }
}