using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Inputs
{
    public class GeraTicketPortariaDesembarqueParaEntradaDepositoCommand : GeraTicketPortariaCommand
    {
        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommand(Guid filialId, int tipoGrao, string placaNumero,
            Guid motoristaId)
        {
            FilialId = filialId;
            TipoGrao = tipoGrao;
            TipoOperacaoPortaria = Enums.TipoOperacaoPortaria.DesembarqueParaEntradaDeposito.Value;
            PlacaNumero = placaNumero;
            MotoristaId = motoristaId;

            NotasFiscais = new List<GeraTicketPortariaNotaFiscalCommand>();
        }
    }
}