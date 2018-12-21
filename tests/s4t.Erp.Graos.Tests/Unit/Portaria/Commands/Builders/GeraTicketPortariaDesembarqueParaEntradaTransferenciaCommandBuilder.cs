using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Builders
{
    public class GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder
    {
        private Guid _filialId = Guid.Empty;
        private int _tipoGrao;
        private string _placaNumero = String.Empty;
        private Guid _motoristaId = Guid.Empty;
        private List<GeraTicketPortariaNotaFiscalCommand> _notasFiscais = new List<GeraTicketPortariaNotaFiscalCommand>();

        public GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand Build()
        {
            var geraTicketDesembarqueParaEntradaTransferenciaCommand = new GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand(_filialId, _tipoGrao, _placaNumero, _motoristaId);

            foreach (var geraTicketNotaFiscalCommand in _notasFiscais)
            {
                geraTicketDesembarqueParaEntradaTransferenciaCommand.NotasFiscais.Add(geraTicketNotaFiscalCommand);
            }

            return geraTicketDesembarqueParaEntradaTransferenciaCommand;
        }

        public GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder ComTipoGrao(int tipoGrao)
        {
            this._tipoGrao = tipoGrao;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder ComPlacaNumero(string placaNumero)
        {
            this._placaNumero = placaNumero;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder ComMotoristaId(Guid motoristaId)
        {
            this._motoristaId = motoristaId;
            return this;
        }

        public GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder ComNotasFiscais(List<GeraTicketPortariaNotaFiscalCommand> notasFiscais)
        {
            this._notasFiscais = notasFiscais;
            return this;
        }

        public static implicit operator GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand(
            GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommandBuilder instance)
        {
            return instance.Build();
        }
    }
}