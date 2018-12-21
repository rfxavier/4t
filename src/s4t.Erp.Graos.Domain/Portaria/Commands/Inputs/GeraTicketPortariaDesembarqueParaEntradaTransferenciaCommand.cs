using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Inputs
{
    public class GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand : GeraTicketPortariaCommand
    {
        public GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand(Guid filialId, int tipoGrao,
            string placaNumero, Guid motoristaId)
        {
            FilialId = filialId;
            TipoGrao = tipoGrao;
            TipoOperacaoPortaria = Enums.TipoOperacaoPortaria.DesembarqueParaEntradaTransferencia.Value;
            PlacaNumero = placaNumero;
            MotoristaId = motoristaId;

            NotasFiscais = new List<GeraTicketPortariaNotaFiscalCommand>();
        }
    }
}