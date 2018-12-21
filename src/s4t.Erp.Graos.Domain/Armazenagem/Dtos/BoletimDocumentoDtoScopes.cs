using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;

namespace s4t.Erp.Graos.Domain.Armazenagem.Dtos
{
    public static class BoletimDocumentoDtoScopes
    {
        public static BoletimDocumento ObterBoletimDocumento(this BoletimDocumentoDto boletimDocumentoDto,
            IDocumentoEntradaRepository documentoEntradaRepository,
            IInstrucaoServicoRepository instrucaoServicoRepository,
            ITransferenciaRepository transferenciaRepository,
            IOrdemCarregamentoRepository ordemCarregamentoRepository)
        {
            DocumentoEntrada documentoEntrada = null;
            InstrucaoServico instrucaoServico = null;
            Transferencia transferencia = null;
            OrdemCarregamento ordemCarregamento = null;
            int remocaoNumero = 0;

            var boletimSerie = BoletimSerie.FromValue(boletimDocumentoDto.Serie.ToUpper());

            if (boletimSerie != null)
            {
                if (boletimSerie == BoletimSerie.Nde)
                {
                    documentoEntrada = documentoEntradaRepository.ObterPorNumero(boletimDocumentoDto.FilialId,
                        boletimDocumentoDto.Numero);
                }

                if (boletimSerie == BoletimSerie.Is)
                {
                    instrucaoServico = instrucaoServicoRepository.ObterPorNumero(boletimDocumentoDto.FilialId, boletimDocumentoDto.Numero);
                }

                if (boletimSerie == BoletimSerie.Tis || boletimSerie == BoletimSerie.Tr)
                {
                    transferencia = transferenciaRepository.ObterPorNumero(boletimDocumentoDto.FilialId, boletimDocumentoDto.Numero);
                }

                if (boletimSerie == BoletimSerie.Oc)
                {
                    ordemCarregamento = ordemCarregamentoRepository.ObterPorNumero(boletimDocumentoDto.FilialId, boletimDocumentoDto.Numero);
                }

                if (boletimSerie == BoletimSerie.Re)
                {
                    remocaoNumero = boletimDocumentoDto.Numero;
                }

            }

            //var boletimDocumento = new BoletimDocumento(boletimSerie, documentoEntradaId, instrucaoServicoId,
            //    transferenciaIsId, transferenciaId, ordemCarregamentoId);

            var boletimDocumento = new BoletimDocumento(boletimSerie, documentoEntrada, instrucaoServico, transferencia, ordemCarregamento, remocaoNumero);

            return boletimDocumento;
        }
    }
}