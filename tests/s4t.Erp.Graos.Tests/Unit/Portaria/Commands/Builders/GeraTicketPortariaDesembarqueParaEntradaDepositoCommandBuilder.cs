using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Builders
{
    public class GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder
    {
        private Guid _filialId = Guid.Empty;
        private int _tipoGrao;
        private string _placaNumero = String.Empty;
        private Guid _motoristaId = Guid.Empty;
        private List<GeraTicketPortariaNotaFiscalCommand> _notasFiscais = new List<GeraTicketPortariaNotaFiscalCommand>();

        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommand Build()
        {
            var geraTicketDesembarqueParaEntradaDepositoCommand = new GeraTicketPortariaDesembarqueParaEntradaDepositoCommand(_filialId, _tipoGrao, _placaNumero, _motoristaId);

            foreach (var geraTicketNotaFiscalCommand in _notasFiscais)
            {
                geraTicketDesembarqueParaEntradaDepositoCommand.NotasFiscais.Add(geraTicketNotaFiscalCommand);
            }

            return geraTicketDesembarqueParaEntradaDepositoCommand;
        }

        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder ComTipoGrao(int tipoGrao)
        {
            this._tipoGrao = tipoGrao;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder ComPlacaNumero(string placaNumero)
        {
            this._placaNumero = placaNumero;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder ComMotoristaId(Guid motoristaId)
        {
            this._motoristaId = motoristaId;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder ComNotasFiscais(List<GeraTicketPortariaNotaFiscalCommand> notasFiscais)
        {
            this._notasFiscais = notasFiscais;
            return this;
        }

        public static implicit operator GeraTicketPortariaDesembarqueParaEntradaDepositoCommand(
            GeraTicketPortariaDesembarqueParaEntradaDepositoCommandBuilder instance)
        {
            return instance.Build();
        }
    }
}