using s4t.Erp.Core.Domain.Commands;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Inputs
{
    public abstract class GeraTicketPortariaCommand : ICommand
    {
        public Guid FilialId { get; protected set; }
        public int TipoGrao { get; protected set; }
        public int TipoOperacaoPortaria { get; protected set; }
        public string PlacaNumero { get; protected set; }
        public Guid MotoristaId { get; protected set; }
        public IList<GeraTicketPortariaNotaFiscalCommand> NotasFiscais { get; set; }
    }
}