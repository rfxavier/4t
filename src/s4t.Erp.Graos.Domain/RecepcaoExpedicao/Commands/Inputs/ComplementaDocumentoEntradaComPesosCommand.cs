using s4t.Erp.Core.Domain.Commands;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs
{
    public class ComplementaDocumentoEntradaComPesosCommand : ICommand
    {
        public Guid DocumentoEntradaId { get; set; }

        public IList<ComplementaDocumentoEntradaComPesosLoteCommand> Lotes { get; set; }

    }
}